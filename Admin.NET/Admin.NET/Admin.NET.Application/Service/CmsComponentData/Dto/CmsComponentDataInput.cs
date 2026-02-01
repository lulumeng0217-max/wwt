// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Admin.NET.Application;

/// <summary>
/// cmscontentdata基础输入参数
/// </summary>
public class CmsComponentDataBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "不能为空")]
    public virtual long? CmsComponentId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string? Title { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string? Subtitle { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string? Content { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string? Icon { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string? Props { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string? LinkUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string? ImageUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual string? BgColor { get; set; }
    
}

/// <summary>
/// cmscontentdata分页查询输入参数
/// </summary>
public class PageCmsComponentDataInput : BasePageInput
{
    /// <summary>
    /// 
    /// </summary>
    public long? CmsComponentId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Subtitle { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Icon { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Props { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? LinkUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? BgColor { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// cmscontentdata增加输入参数
/// </summary>
public class AddCmsComponentDataInput
{
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "不能为空")]
    public long? CmsComponentId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(200, ErrorMessage = "字符长度不能超过200")]
    public string? Title { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(200, ErrorMessage = "字符长度不能超过200")]
    public string? Subtitle { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(255, ErrorMessage = "字符长度不能超过255")]
    public string? Content { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100, ErrorMessage = "字符长度不能超过100")]
    public string? Icon { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(500, ErrorMessage = "字符长度不能超过500")]
    public string? Props { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(255, ErrorMessage = "字符长度不能超过255")]
    public string? LinkUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(255, ErrorMessage = "字符长度不能超过255")]
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(20, ErrorMessage = "字符长度不能超过20")]
    public string? BgColor { get; set; }
    
}

/// <summary>
/// cmscontentdata删除输入参数
/// </summary>
public class DeleteCmsComponentDataInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// cmscontentdata更新输入参数
/// </summary>
public class UpdateCmsComponentDataInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [Required(ErrorMessage = "不能为空")]
    public long? CmsComponentId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [MaxLength(200, ErrorMessage = "字符长度不能超过200")]
    public string? Title { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [MaxLength(200, ErrorMessage = "字符长度不能超过200")]
    public string? Subtitle { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [MaxLength(255, ErrorMessage = "字符长度不能超过255")]
    public string? Content { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [MaxLength(100, ErrorMessage = "字符长度不能超过100")]
    public string? Icon { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [MaxLength(500, ErrorMessage = "字符长度不能超过500")]
    public string? Props { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [MaxLength(255, ErrorMessage = "字符长度不能超过255")]
    public string? LinkUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [MaxLength(255, ErrorMessage = "字符长度不能超过255")]
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>    
    [MaxLength(20, ErrorMessage = "字符长度不能超过20")]
    public string? BgColor { get; set; }
    
}

/// <summary>
/// cmscontentdata主键查询输入参数
/// </summary>
public class QueryByIdCmsComponentDataInput : DeleteCmsComponentDataInput
{
}

/// <summary>
/// cmscontentdata数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportCmsComponentDataInput : BaseImportInput
{
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "*")]
    [ExporterHeader("*", Format = "", Width = 25, IsBold = true)]
    public long? CmsComponentId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "")]
    [ExporterHeader("", Format = "", Width = 25, IsBold = true)]
    public string? Title { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "")]
    [ExporterHeader("", Format = "", Width = 25, IsBold = true)]
    public string? Subtitle { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "")]
    [ExporterHeader("", Format = "", Width = 25, IsBold = true)]
    public string? Content { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "")]
    [ExporterHeader("", Format = "", Width = 25, IsBold = true)]
    public string? Icon { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "")]
    [ExporterHeader("", Format = "", Width = 25, IsBold = true)]
    public string? Props { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "")]
    [ExporterHeader("", Format = "", Width = 25, IsBold = true)]
    public string? LinkUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "")]
    [ExporterHeader("", Format = "", Width = 25, IsBold = true)]
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [ImporterHeader(Name = "")]
    [ExporterHeader("", Format = "", Width = 25, IsBold = true)]
    public string? BgColor { get; set; }
    
}
