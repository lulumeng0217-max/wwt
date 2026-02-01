using Admin.NET.Core.Service;
using Microsoft.AspNetCore.Http;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Mapster;
using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Admin.NET.Application.Entity;
using Yitter.IdGenerator;
namespace Admin.NET.Application;

/// <summary>
/// component服务 🧩
/// </summary>
[ApiDescriptionSettings("WWT-CMS", Module = "WWT-CMS", Order = 200)]
public partial class CmsComponentService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<CmsComponent> _cmsComponentRep;
    private readonly ISqlSugarClient _sqlSugarClient;

    public CmsComponentService(SqlSugarRepository<CmsComponent> cmsComponentRep, ISqlSugarClient sqlSugarClient)
    {
        _cmsComponentRep = cmsComponentRep;
        _sqlSugarClient = sqlSugarClient;
    }

    /// <summary>
    /// 分页查询component 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询component")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<CmsComponentOutput>> Page(PageCmsComponentInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _cmsComponentRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Props.Contains(input.Keyword) || u.Styles.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Props), u => u.Props.Contains(input.Props.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Styles), u => u.Styles.Contains(input.Styles.Trim()))
            .WhereIF(input.PageId != null, u => u.PageId == input.PageId)
            .WhereIF(input.ComponentTypeId != null, u => u.ComponentTypeId == input.ComponentTypeId)
            .WhereIF(input.Pid != null, u => u.Pid == input.Pid)
            .WhereIF(input.SortOrder != null, u => u.SortOrder == input.SortOrder)
            .WhereIF(input.IsVisible.HasValue, u => u.IsVisible == input.IsVisible)
            .WhereIF(input.DeleteTimeRange?.Length == 2, u => u.DeleteTime >= input.DeleteTimeRange[0] && u.DeleteTime <= input.DeleteTimeRange[1])
            .Select<CmsComponentOutput>();
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取component详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取component详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<CmsComponent> Detail([FromQuery] QueryByIdCmsComponentInput input)
    {
        return await _cmsComponentRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加component ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("add component")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddCmsComponentInput input)
    {
        if (input.PageId==null)
            throw Oops.Oh("PageId is required");
        var entity = input.Adapt<CmsComponent>();
        return await _cmsComponentRep.InsertAsync(entity) ? entity.Id : 0;
    }
    /// <summary>
    /// add component ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("add component batch")]
    [ApiDescriptionSettings(Name = "BatchAdd"), HttpPost]
    public async Task BatchAdd(AddCmsComponentBatchInput input)
    {
        await _sqlSugarClient.AsTenant().BeginTranAsync();
        if (input.ComponentList == null || input.ComponentList.Count == 0)
            return;
        if (!input.PageId.HasValue)
            throw Oops.Oh("PageId is required");
        var idMap = new Dictionary<string, long>();
        var entities = new List<CmsComponent>();
        foreach (var item in input.ComponentList)
        {
            var dbId = YitIdHelper.NextId();
            idMap[item.Id] = dbId;
            entities.Add(new CmsComponent
            {
                Id = dbId,
                PageId = input.PageId.Value,
                ComponentTypeId = item.ComponentTypeId!.Value,
                SortOrder = item.SortOrder!.Value,
                IsVisible = item.IsVisible ?? true,
                Props = item.Props,
                Styles = item.Styles ?? string.Empty,
            });
        }
        for (int i = 0; i < input.ComponentList.Count; i++)
        {
            var inputItem = input.ComponentList[i];
            var entity = entities[i];
            if (string.IsNullOrEmpty(inputItem.Pid) || inputItem.Pid == "0")
            {
                entity.Pid = 0;
            }
            else
            {
                if (!idMap.TryGetValue(inputItem.Pid, out var parentId))
                    throw new Exception($"非法 pid：{inputItem.Pid}");

                entity.Pid = parentId;
            }
        }
        await _cmsComponentRep.DeleteAsync(s=>s.PageId==input.PageId);
        await _cmsComponentRep.InsertRangeAsync(entities);
        await _sqlSugarClient.AsTenant().CommitTranAsync();
    }
    /// <summary>
    /// 更新component ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName(" update component")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateCmsComponentInput input)
    {
        var entity = input.Adapt<CmsComponent>();
        await _cmsComponentRep.AsUpdateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除component ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("remove component")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteCmsComponentInput input)
    {
        var entity = await _cmsComponentRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _cmsComponentRep.DeleteAsync(entity);   //真删除 
    }

    /// <summary>
    /// 批量删除component ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除component")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")] List<DeleteCmsComponentInput> input)
    {
        var exp = Expressionable.Create<CmsComponent>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _cmsComponentRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _cmsComponentRep.Context.Deleteable(list).ExecuteCommandAsync();   //真删除--返回受影响的行数
    }
    /// <summary>
    /// GetCmsComponent
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns></returns>
    [DisplayName("GetCmsComponent")]
    [ApiDescriptionSettings(Name = "GetCmsComponent"), HttpGet]
    public async Task<List<CmsComponentDto>> GetCmsComponent(long pageId)
    {
        return await _cmsComponentRep.AsQueryable()
             .LeftJoin<CmsComponentType>((a, b) => a.ComponentTypeId == b.Id)
              .Where(a => a.PageId == pageId)
             .Select((a, b) => new CmsComponentDto
             {
                 Id = a.Id,
                 Pid = a.Pid,
                 PageId = a.PageId,
                 Props = a.Props,
                 Styles = a.Styles,
                 SortOrder = a.SortOrder,
                 IsVisible = a.IsVisible,
                 ComponentTypeId=a.ComponentTypeId,
                 CmsComponentType = new CmsComponentTypeDto
                 {
                     Id = b.Id,
                     Name = b.Name,
                     DefaultProps = b.DefaultProps,
                     SetStyles = b.SetStyles,
                     ComponentKind = b.ComponentKind,
                     Fields = b.Fields
                 }

             }).ToListAsync();
        //return cmsComponent.ToTree(it => it.Children, it => it.Pid, 0).ToList();

    }


}
