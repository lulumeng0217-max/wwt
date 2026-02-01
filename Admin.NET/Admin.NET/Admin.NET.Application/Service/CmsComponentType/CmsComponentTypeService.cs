// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

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
/// component_type服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public partial class CmsComponentTypeService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<CmsComponentType> _cmsComponentTypeRep;
    private readonly ISqlSugarClient _sqlSugarClient;

    public CmsComponentTypeService(SqlSugarRepository<CmsComponentType> cmsComponentTypeRep, ISqlSugarClient sqlSugarClient)
    {
        _cmsComponentTypeRep = cmsComponentTypeRep;
        _sqlSugarClient = sqlSugarClient;
    }

    /// <summary>
    /// 分页查询component_type 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询component_type")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<CmsComponentTypeOutput>> Page(PageCmsComponentTypeInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _cmsComponentTypeRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Name.Contains(input.Keyword) || u.Description.Contains(input.Keyword) || u.DefaultProps.Contains(input.Keyword) || u.SetStyles.Contains(input.Keyword) || u.Fields.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Description), u => u.Description.Contains(input.Description.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.DefaultProps), u => u.DefaultProps.Contains(input.DefaultProps.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.SetStyles), u => u.SetStyles.Contains(input.SetStyles.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Fields), u => u.Fields.Contains(input.Fields.Trim()))
            .WhereIF(input.ComponentKind.HasValue, u => u.ComponentKind == input.ComponentKind)
            .WhereIF(input.Status != null, u => u.Status == input.Status)
            .WhereIF(input.DeleteTimeRange?.Length == 2, u => u.DeleteTime >= input.DeleteTimeRange[0] && u.DeleteTime <= input.DeleteTimeRange[1])
            .Select<CmsComponentTypeOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取component_type详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取component_type详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<CmsComponentType> Detail([FromQuery] QueryByIdCmsComponentTypeInput input)
    {
        return await _cmsComponentTypeRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加component_type ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加component_type")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddCmsComponentTypeInput input)
    {
        var entity = input.Adapt<CmsComponentType>();
        return await _cmsComponentTypeRep.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 更新component_type ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新component_type")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateCmsComponentTypeInput input)
    {
        var entity = input.Adapt<CmsComponentType>();
        await _cmsComponentTypeRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除component_type ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除component_type")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteCmsComponentTypeInput input)
    {
        var entity = await _cmsComponentTypeRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002); 
        await _cmsComponentTypeRep.DeleteAsync(entity);   //真删除 
    }

    /// <summary>
    /// 批量删除component_type ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除component_type")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteCmsComponentTypeInput> input)
    {
        var exp = Expressionable.Create<CmsComponentType>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _cmsComponentTypeRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _cmsComponentTypeRep.Context.Deleteable(list).ExecuteCommandAsync();   //真删除--返回受影响的行数
    }
    
    /// <summary>
    /// 导出component_type记录 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("导出component_type记录")]
    [ApiDescriptionSettings(Name = "Export"), HttpPost, NonUnify]
    public async Task<IActionResult> Export(PageCmsComponentTypeInput input)
    {
        var list = (await Page(input)).Items?.Adapt<List<ExportCmsComponentTypeOutput>>() ?? new();
        if (input.SelectKeyList?.Count > 0) list = list.Where(x => input.SelectKeyList.Contains(x.Id)).ToList();
        return ExcelHelper.ExportTemplate(list, "component_type导出记录");
    }
    
    /// <summary>
    /// 下载component_type数据导入模板 ⬇️
    /// </summary>
    /// <returns></returns>
    [DisplayName("下载component_type数据导入模板")]
    [ApiDescriptionSettings(Name = "Import"), HttpGet, NonUnify]
    public IActionResult DownloadTemplate()
    {
        return ExcelHelper.ExportTemplate(new List<ExportCmsComponentTypeOutput>(), "component_type导入模板");
    }
    
    private static readonly object _cmsComponentTypeImportLock = new object();
    /// <summary>
    /// 导入component_type记录 💾
    /// </summary>
    /// <returns></returns>
    [DisplayName("导入component_type记录")]
    [ApiDescriptionSettings(Name = "Import"), HttpPost, NonUnify, UnitOfWork]
    public IActionResult ImportData([Required] IFormFile file)
    {
        lock (_cmsComponentTypeImportLock)
        {
            var stream = ExcelHelper.ImportData<ImportCmsComponentTypeInput, CmsComponentType>(file, (list, markerErrorAction) =>
            {
                _sqlSugarClient.Utilities.PageEach(list, 2048, pageItems =>
                {
                    
                    // 校验并过滤必填基本类型为null的字段
                    var rows = pageItems.Where(x => {
                        if (!string.IsNullOrWhiteSpace(x.Error)) return false;
                        if (x.ComponentKind == null){
                            x.Error = "ComponentKind不能为空";
                            return false;
                        }
                        if (x.Status == null){
                            x.Error = "Status不能为空";
                            return false;
                        }
                        return true;
                    }).Adapt<List<CmsComponentType>>();
                    
                    var storageable = _cmsComponentTypeRep.Context.Storageable(rows)
                        .SplitError(it => string.IsNullOrWhiteSpace(it.Item.Name), "Name不能为空")
                        .SplitError(it => it.Item.Name?.Length > 255, "Name长度不能超过255个字符")
                        .SplitError(it => it.Item.Description?.Length > 255, "Description长度不能超过255个字符")
                        .SplitError(it => it.Item.DefaultProps?.Length > 500, "DefaultProps长度不能超过500个字符")
                        .SplitError(it => it.Item.SetStyles?.Length > 500, "SetStyles长度不能超过500个字符")
                        .SplitError(it => it.Item.Fields?.Length > 500, "Fields长度不能超过500个字符")
                        .SplitInsert(_=> true) // 没有设置唯一键代表插入所有数据
                        .ToStorage();
                    
                    storageable.AsInsertable.ExecuteCommand();// 不存在插入
                    storageable.AsUpdateable.UpdateColumns(it => new
                    {
                        it.Name,
                        it.ComponentKind,
                        it.Description,
                        it.DefaultProps,
                        it.SetStyles,
                        it.Fields,
                        it.Status,
                        it.DeleteTime,
                    }).ExecuteCommand();// 存在更新
                    
                    // 标记错误信息
                    markerErrorAction.Invoke(storageable, pageItems, rows);
                });
            });
            
            return stream;
        }
    }
}
