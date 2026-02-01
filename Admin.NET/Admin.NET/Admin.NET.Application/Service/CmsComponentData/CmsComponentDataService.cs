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
/// cmscontentdata服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public partial class CmsComponentDataService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<CmsComponentData> _cmsComponentDataRep;
    private readonly ISqlSugarClient _sqlSugarClient;

    public CmsComponentDataService(SqlSugarRepository<CmsComponentData> cmsComponentDataRep, ISqlSugarClient sqlSugarClient)
    {
        _cmsComponentDataRep = cmsComponentDataRep;
        _sqlSugarClient = sqlSugarClient;
    }

    /// <summary>
    /// 分页查询cmscontentdata 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询cmscontentdata")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<CmsComponentDataOutput>> Page(PageCmsComponentDataInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _cmsComponentDataRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Title.Contains(input.Keyword) || u.Subtitle.Contains(input.Keyword) || u.Content.Contains(input.Keyword) || u.Icon.Contains(input.Keyword) || u.Props.Contains(input.Keyword) || u.LinkUrl.Contains(input.Keyword) || u.ImageUrl.Contains(input.Keyword) || u.BgColor.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Title), u => u.Title.Contains(input.Title.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Subtitle), u => u.Subtitle.Contains(input.Subtitle.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Content), u => u.Content.Contains(input.Content.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Icon), u => u.Icon.Contains(input.Icon.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Props), u => u.Props.Contains(input.Props.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.LinkUrl), u => u.LinkUrl.Contains(input.LinkUrl.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ImageUrl), u => u.ImageUrl.Contains(input.ImageUrl.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.BgColor), u => u.BgColor.Contains(input.BgColor.Trim()))
            .WhereIF(input.CmsComponentId != null, u => u.CmsComponentId == input.CmsComponentId)
            .Select<CmsComponentDataOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取cmscontentdata详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取cmscontentdata详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<CmsComponentData> Detail([FromQuery] QueryByIdCmsComponentDataInput input)
    {
        return await _cmsComponentDataRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加cmscontentdata ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加cmscontentdata")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddCmsComponentDataInput input)
    {
        var entity = input.Adapt<CmsComponentData>();
        return await _cmsComponentDataRep.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 更新cmscontentdata ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新cmscontentdata")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateCmsComponentDataInput input)
    {
        var entity = input.Adapt<CmsComponentData>();
        await _cmsComponentDataRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除cmscontentdata ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除cmscontentdata")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteCmsComponentDataInput input)
    {
        var entity = await _cmsComponentDataRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002); 
        await _cmsComponentDataRep.DeleteAsync(entity);   //真删除 
    }

    /// <summary>
    /// 批量删除cmscontentdata ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除cmscontentdata")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteCmsComponentDataInput> input)
    {
        var exp = Expressionable.Create<CmsComponentData>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _cmsComponentDataRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _cmsComponentDataRep.Context.Deleteable(list).ExecuteCommandAsync();   //真删除--返回受影响的行数
    }
    
    /// <summary>
    /// 导出cmscontentdata记录 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("导出cmscontentdata记录")]
    [ApiDescriptionSettings(Name = "Export"), HttpPost, NonUnify]
    public async Task<IActionResult> Export(PageCmsComponentDataInput input)
    {
        var list = (await Page(input)).Items?.Adapt<List<ExportCmsComponentDataOutput>>() ?? new();
        if (input.SelectKeyList?.Count > 0) list = list.Where(x => input.SelectKeyList.Contains(x.Id)).ToList();
        return ExcelHelper.ExportTemplate(list, "cmscontentdata导出记录");
    }
    
    /// <summary>
    /// 下载cmscontentdata数据导入模板 ⬇️
    /// </summary>
    /// <returns></returns>
    [DisplayName("下载cmscontentdata数据导入模板")]
    [ApiDescriptionSettings(Name = "Import"), HttpGet, NonUnify]
    public IActionResult DownloadTemplate()
    {
        return ExcelHelper.ExportTemplate(new List<ExportCmsComponentDataOutput>(), "cmscontentdata导入模板");
    }
    
    private static readonly object _cmsComponentDataImportLock = new object();
    /// <summary>
    /// 导入cmscontentdata记录 💾
    /// </summary>
    /// <returns></returns>
    [DisplayName("导入cmscontentdata记录")]
    [ApiDescriptionSettings(Name = "Import"), HttpPost, NonUnify, UnitOfWork]
    public IActionResult ImportData([Required] IFormFile file)
    {
        lock (_cmsComponentDataImportLock)
        {
            var stream = ExcelHelper.ImportData<ImportCmsComponentDataInput, CmsComponentData>(file, (list, markerErrorAction) =>
            {
                _sqlSugarClient.Utilities.PageEach(list, 2048, pageItems =>
                {
                    
                    // 校验并过滤必填基本类型为null的字段
                    var rows = pageItems.Where(x => {
                        if (!string.IsNullOrWhiteSpace(x.Error)) return false;
                        if (x.CmsComponentId == null){
                            x.Error = "不能为空";
                            return false;
                        }
                        return true;
                    }).Adapt<List<CmsComponentData>>();
                    
                    var storageable = _cmsComponentDataRep.Context.Storageable(rows)
                        .SplitError(it => it.Item.Title?.Length > 200, "长度不能超过200个字符")
                        .SplitError(it => it.Item.Subtitle?.Length > 200, "长度不能超过200个字符")
                        .SplitError(it => it.Item.Content?.Length > 255, "长度不能超过255个字符")
                        .SplitError(it => it.Item.Icon?.Length > 100, "长度不能超过100个字符")
                        .SplitError(it => it.Item.Props?.Length > 500, "长度不能超过500个字符")
                        .SplitError(it => it.Item.LinkUrl?.Length > 255, "长度不能超过255个字符")
                        .SplitError(it => it.Item.ImageUrl?.Length > 255, "长度不能超过255个字符")
                        .SplitError(it => it.Item.BgColor?.Length > 20, "长度不能超过20个字符")
                        .SplitInsert(_=> true) // 没有设置唯一键代表插入所有数据
                        .ToStorage();
                    
                    storageable.AsInsertable.ExecuteCommand();// 不存在插入
                    storageable.AsUpdateable.UpdateColumns(it => new
                    {
                        it.CmsComponentId,
                        it.Title,
                        it.Subtitle,
                        it.Content,
                        it.Icon,
                        it.Props,
                        it.LinkUrl,
                        it.ImageUrl,
                        it.BgColor,
                    }).ExecuteCommand();// 存在更新
                    
                    // 标记错误信息
                    markerErrorAction.Invoke(storageable, pageItems, rows);
                });
            });
            
            return stream;
        }
    }
}
