using Admin.NET.Core.Service;
using Microsoft.AspNetCore.Http;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Mapster;
using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Admin.NET.Application.Entity;
namespace Admin.NET.Application;

/// <summary>
/// page服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public partial class CmsPageService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<CmsPage> _cmsPageRep;
    private readonly SqlSugarRepository<CmsComponent> _cmsCmsComponent;
    private readonly SqlSugarRepository<CmsComponentType> _cmsCmsComponentType;
    private readonly ISqlSugarClient _sqlSugarClient;

    public CmsPageService(SqlSugarRepository<CmsPage> cmsPageRep, ISqlSugarClient sqlSugarClient,SqlSugarRepository<CmsComponentType> cmsCmsComponentType, SqlSugarRepository<CmsComponent> cmsCmsComponent)
    {
        _cmsPageRep = cmsPageRep;
        _sqlSugarClient = sqlSugarClient;
        _cmsCmsComponentType = cmsCmsComponentType;
        _cmsCmsComponent = cmsCmsComponent;
    }

    /// <summary>
    /// 分页查询page 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询page")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<CmsPageOutput>> Page(PageCmsPageInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _cmsPageRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Pagetype.Contains(input.Keyword) || u.Title.Contains(input.Keyword) || u.SubTitle.Contains(input.Keyword) || u.RequestPath.Contains(input.Keyword) || u.RealPath.Contains(input.Keyword) || u.Description.Contains(input.Keyword) || u.Keywords.Contains(input.Keyword) || u.CanonicalUrl.Contains(input.Keyword) || u.Robots.Contains(input.Keyword) || u.OgTitle.Contains(input.Keyword) || u.OgImage.Contains(input.Keyword) || u.OgType.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Pagetype), u => u.Pagetype.Contains(input.Pagetype.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Title), u => u.Title.Contains(input.Title.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.SubTitle), u => u.SubTitle.Contains(input.SubTitle.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.RequestPath), u => u.RequestPath.Contains(input.RequestPath.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.RealPath), u => u.RealPath.Contains(input.RealPath.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Description), u => u.Description.Contains(input.Description.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keywords), u => u.Keywords.Contains(input.Keywords.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.CanonicalUrl), u => u.CanonicalUrl.Contains(input.CanonicalUrl.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Robots), u => u.Robots.Contains(input.Robots.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.OgTitle), u => u.OgTitle.Contains(input.OgTitle.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.OgImage), u => u.OgImage.Contains(input.OgImage.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.OgType), u => u.OgType.Contains(input.OgType.Trim()))
            .WhereIF(input.TemplateId != null, u => u.TemplateId == input.TemplateId)
            .WhereIF(input.Status != null, u => u.Status == input.Status)
            .WhereIF(input.IsDynamic.HasValue, u => u.IsDynamic == input.IsDynamic)
            .WhereIF(input.DeleteTimeRange?.Length == 2, u => u.DeleteTime >= input.DeleteTimeRange[0] && u.DeleteTime <= input.DeleteTimeRange[1])
            .Select<CmsPageOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取page详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取page详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<CmsPage> Detail([FromQuery] QueryByIdCmsPageInput input)
    {
        return await _cmsPageRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// ad page ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("add page")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddCmsPageInput input)
    {
        var page= await _cmsPageRep.IsAnyAsync(p => p.RequestPath == input.RequestPath);
        if (page)
            throw Oops.Oh("request path  already exists");
        try
        {
            await _sqlSugarClient.AsTenant().BeginTranAsync();
            var cmsPage = await _cmsPageRep.InsertReturnEntityAsync(input.Adapt<CmsPage>());
            if (input.TemplateId != null)
            {
                //get tempalte info
                var template = await _cmsCmsComponent.GetListAsync(u => u.PageId == input.TemplateId);
                if (template != null && template.Count > 0)
                {
                    foreach (var item in template)
                    {
                        var newComponent = item;
                        newComponent.PageId = cmsPage.Id;
                        await _cmsCmsComponent.InsertAsync(newComponent);
                    }
                }
            }
            await _sqlSugarClient.AsTenant().CommitTranAsync();
            return cmsPage.Id;
        }
        catch (Exception ex)
        {
            await _sqlSugarClient.AsTenant().RollbackTranAsync();
            throw Oops.Oh(ex);
        }
      
    }

    /// <summary>
    /// update page ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("update page")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateCmsPageInput input)
    {
        try
        {
            await _sqlSugarClient.AsTenant().BeginTranAsync();
            var entity = input.Adapt<CmsPage>();
            await _cmsPageRep.AsUpdateable(entity).ExecuteCommandAsync();
            if (input.TemplateId != null)
            {
                await _cmsCmsComponent.DeleteAsync(u => u.PageId == entity.Id);
                //get tempalte info
                var template = await _cmsCmsComponent.GetListAsync(u => u.PageId == input.TemplateId);
                if (template != null && template.Count > 0)
                {
                    foreach (var item in template)
                    {
                        var newComponent = item;
                        newComponent.PageId = entity.Id;
                        await _cmsCmsComponent.InsertAsync(newComponent);
                    }
                }
            }
            await _sqlSugarClient.AsTenant().CommitTranAsync();
        }
        catch (Exception)
        {

            throw;
        }
       
    }

    /// <summary>
    /// 删除page ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除page")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteCmsPageInput input)
    {
        var entity = await _cmsPageRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002); 
        await _cmsPageRep.DeleteAsync(entity);   //真删除 
    }

    /// <summary>
    /// 批量删除page ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除page")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteCmsPageInput> input)
    {
        var exp = Expressionable.Create<CmsPage>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _cmsPageRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _cmsPageRep.Context.Deleteable(list).ExecuteCommandAsync();   //真删除--返回受影响的行数
    }


    /// <summary>
    /// tempalte 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("tempalte")]
    [ApiDescriptionSettings(Name = "GetPageTemplate"), HttpPost]
    public async Task<List<CmsPageOutput>> GetPageTemplate()
    {
        var query = await _cmsPageRep.AsQueryable().IgnoreTenant(true).Where(u => u.Pagetype == "template").ToListAsync();
        return query.Adapt<List<CmsPageOutput>>();
    }

    /// <summary>
    /// GetCmsComponent
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns></returns>
    [DisplayName("GetCmsComponent")]
    [ApiDescriptionSettings(Name = "GetCmsComponent"), HttpGet]
    public async Task<List<CmsPageDto>> GetCmsPageTree()
    {
        var cmsPage = await _cmsPageRep.AsQueryable().ToListAsync();
        var cmsPageList = cmsPage.Adapt<List<CmsPageDto>>();
        return cmsPageList.ToTree(it => it.Children, it => it.Pid, 0).ToList();

    }
}
