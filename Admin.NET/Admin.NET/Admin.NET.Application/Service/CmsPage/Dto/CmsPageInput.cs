using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Admin.NET.Application;

/// <summary>
/// page基础输入参数
/// </summary>
public class CmsPageBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// Pagetype
    /// </summary>
    [Required(ErrorMessage = "Pagetype不能为空")]
    public virtual string Pagetype { get; set; }
    
    /// <summary>
    /// TemplateId
    /// </summary>
    [Required(ErrorMessage = "TemplateId不能为空")]
    public virtual long? TemplateId { get; set; }
    
    /// <summary>
    /// Title
    /// </summary>
    public virtual string? Title { get; set; }
    
    /// <summary>
    /// SubTitle
    /// </summary>
    [Required(ErrorMessage = "SubTitle不能为空")]
    public virtual string SubTitle { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    [Required(ErrorMessage = "Status不能为空")]
    public virtual int? Status { get; set; }
    
    /// <summary>
    /// RequestPath
    /// </summary>
    public virtual string? RequestPath { get; set; }
    
    /// <summary>
    /// RealPath
    /// </summary>
    public virtual string? RealPath { get; set; }
    
    /// <summary>
    /// IsDynamic
    /// </summary>
    [Required(ErrorMessage = "IsDynamic不能为空")]
    public virtual bool? IsDynamic { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public virtual string? Description { get; set; }
    
    /// <summary>
    /// Keywords
    /// </summary>
    public virtual string? Keywords { get; set; }
    
    /// <summary>
    /// CanonicalUrl
    /// </summary>
    public virtual string? CanonicalUrl { get; set; }
    
    /// <summary>
    /// Robots
    /// </summary>
    public virtual string? Robots { get; set; }
    
    /// <summary>
    /// OgTitle
    /// </summary>
    public virtual string? OgTitle { get; set; }
    
    /// <summary>
    /// OgImage
    /// </summary>
    public virtual string? OgImage { get; set; }
    
    /// <summary>
    /// OgType
    /// </summary>
    public virtual string? OgType { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    public virtual DateTime? DeleteTime { get; set; }
    
}

/// <summary>
/// page分页查询输入参数
/// </summary>
public class PageCmsPageInput : BasePageInput
{
    /// <summary>
    /// Pagetype
    /// </summary>
    public string Pagetype { get; set; }
    
    /// <summary>
    /// TemplateId
    /// </summary>
    public long? TemplateId { get; set; }
    
    /// <summary>
    /// Title
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// SubTitle
    /// </summary>
    public string SubTitle { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    public int? Status { get; set; }
    
    /// <summary>
    /// RequestPath
    /// </summary>
    public string? RequestPath { get; set; }
    
    /// <summary>
    /// RealPath
    /// </summary>
    public string? RealPath { get; set; }
    
    /// <summary>
    /// IsDynamic
    /// </summary>
    public bool? IsDynamic { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Keywords
    /// </summary>
    public string? Keywords { get; set; }
    
    /// <summary>
    /// CanonicalUrl
    /// </summary>
    public string? CanonicalUrl { get; set; }
    
    /// <summary>
    /// Robots
    /// </summary>
    public string? Robots { get; set; }
    
    /// <summary>
    /// OgTitle
    /// </summary>
    public string? OgTitle { get; set; }
    
    /// <summary>
    /// OgImage
    /// </summary>
    public string? OgImage { get; set; }
    
    /// <summary>
    /// OgType
    /// </summary>
    public string? OgType { get; set; }
    
    /// <summary>
    /// DeleteTime范围
    /// </summary>
     public DateTime?[] DeleteTimeRange { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// page增加输入参数
/// </summary>
public class AddCmsPageInput
{
    /// <summary>
    /// Pagetype
    /// </summary>
    [Required(ErrorMessage = "Pagetype不能为空")]
    [MaxLength(50, ErrorMessage = "Pagetype字符长度不能超过50")]
    public string Pagetype { get; set; }

    public long Pid { get; set; }

    /// <summary>
    /// TemplateId
    /// </summary>
    public long? TemplateId { get; set; }
    
    /// <summary>
    /// Title
    /// </summary>
    [MaxLength(255, ErrorMessage = "Title字符长度不能超过255")]
    public string? Title { get; set; }
    
    /// <summary>
    /// SubTitle
    /// </summary>
    [Required(ErrorMessage = "SubTitle不能为空")]
    [MaxLength(255, ErrorMessage = "SubTitle字符长度不能超过255")]
    public string SubTitle { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    [Required(ErrorMessage = "Status不能为空")]
    public int? Status { get; set; }
    
    /// <summary>
    /// RequestPath
    /// </summary>
    [MaxLength(255, ErrorMessage = "RequestPath字符长度不能超过255")]
    public string? RequestPath { get; set; }
    
    /// <summary>
    /// RealPath
    /// </summary>
    [MaxLength(255, ErrorMessage = "RealPath字符长度不能超过255")]
    public string? RealPath { get; set; }
    
    /// <summary>
    /// IsDynamic
    /// </summary>
    [Required(ErrorMessage = "IsDynamic不能为空")]
    public bool? IsDynamic { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    [MaxLength(255, ErrorMessage = "Description字符长度不能超过255")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Keywords
    /// </summary>
    [MaxLength(255, ErrorMessage = "Keywords字符长度不能超过255")]
    public string? Keywords { get; set; }
    
    /// <summary>
    /// CanonicalUrl
    /// </summary>
    [MaxLength(255, ErrorMessage = "CanonicalUrl字符长度不能超过255")]
    public string? CanonicalUrl { get; set; }
    
    /// <summary>
    /// Robots
    /// </summary>
    [MaxLength(50, ErrorMessage = "Robots字符长度不能超过50")]
    public string? Robots { get; set; }
    
    /// <summary>
    /// OgTitle
    /// </summary>
    [MaxLength(255, ErrorMessage = "OgTitle字符长度不能超过255")]
    public string? OgTitle { get; set; }
    
    /// <summary>
    /// OgImage
    /// </summary>
    [MaxLength(255, ErrorMessage = "OgImage字符长度不能超过255")]
    public string? OgImage { get; set; }
    
    /// <summary>
    /// OgType
    /// </summary>
    [MaxLength(50, ErrorMessage = "OgType字符长度不能超过50")]
    public string? OgType { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    public DateTime? DeleteTime { get; set; }


    public List<AddCmsComponentInput> AddCmsComponentInput { get; set; }


}

/// <summary>
/// page删除输入参数
/// </summary>
public class DeleteCmsPageInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// page更新输入参数
/// </summary>
public class UpdateCmsPageInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }

    public long Pid { get; set; }

    /// <summary>
    /// Pagetype
    /// </summary>    
    [Required(ErrorMessage = "Pagetype不能为空")]
    [MaxLength(50, ErrorMessage = "Pagetype字符长度不能超过50")]
    public string Pagetype { get; set; }
    
    /// <summary>
    /// TemplateId
    /// </summary>    
    public long? TemplateId { get; set; }
    
    /// <summary>
    /// Title
    /// </summary>    
    [MaxLength(255, ErrorMessage = "Title字符长度不能超过255")]
    public string? Title { get; set; }
    
    /// <summary>
    /// SubTitle
    /// </summary>    
    [Required(ErrorMessage = "SubTitle不能为空")]
    [MaxLength(255, ErrorMessage = "SubTitle字符长度不能超过255")]
    public string SubTitle { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>    
    [Required(ErrorMessage = "Status不能为空")]
    public int? Status { get; set; }
    
    /// <summary>
    /// RequestPath
    /// </summary>    
    [MaxLength(255, ErrorMessage = "RequestPath字符长度不能超过255")]
    public string? RequestPath { get; set; }
    
    /// <summary>
    /// RealPath
    /// </summary>    
    [MaxLength(255, ErrorMessage = "RealPath字符长度不能超过255")]
    public string? RealPath { get; set; }
    
    /// <summary>
    /// IsDynamic
    /// </summary>    
    [Required(ErrorMessage = "IsDynamic不能为空")]
    public bool? IsDynamic { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>    
    [MaxLength(255, ErrorMessage = "Description字符长度不能超过255")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Keywords
    /// </summary>    
    [MaxLength(255, ErrorMessage = "Keywords字符长度不能超过255")]
    public string? Keywords { get; set; }
    
    /// <summary>
    /// CanonicalUrl
    /// </summary>    
    [MaxLength(255, ErrorMessage = "CanonicalUrl字符长度不能超过255")]
    public string? CanonicalUrl { get; set; }
    
    /// <summary>
    /// Robots
    /// </summary>    
    [MaxLength(50, ErrorMessage = "Robots字符长度不能超过50")]
    public string? Robots { get; set; }
    
    /// <summary>
    /// OgTitle
    /// </summary>    
    [MaxLength(255, ErrorMessage = "OgTitle字符长度不能超过255")]
    public string? OgTitle { get; set; }
    
    /// <summary>
    /// OgImage
    /// </summary>    
    [MaxLength(255, ErrorMessage = "OgImage字符长度不能超过255")]
    public string? OgImage { get; set; }
    
    /// <summary>
    /// OgType
    /// </summary>    
    [MaxLength(50, ErrorMessage = "OgType字符长度不能超过50")]
    public string? OgType { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>    
    public DateTime? DeleteTime { get; set; }
    
}

/// <summary>
/// page主键查询输入参数
/// </summary>
public class QueryByIdCmsPageInput : DeleteCmsPageInput
{
}

/// <summary>
/// page数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportCmsPageInput : BaseImportInput
{
    /// <summary>
    /// Pagetype
    /// </summary>
    [ImporterHeader(Name = "*Pagetype")]
    [ExporterHeader("*Pagetype", Format = "", Width = 25, IsBold = true)]
    public string Pagetype { get; set; }
    
    /// <summary>
    /// TemplateId
    /// </summary>
    [ImporterHeader(Name = "*TemplateId")]
    [ExporterHeader("*TemplateId", Format = "", Width = 25, IsBold = true)]
    public long? TemplateId { get; set; }
    
    /// <summary>
    /// Title
    /// </summary>
    [ImporterHeader(Name = "Title")]
    [ExporterHeader("Title", Format = "", Width = 25, IsBold = true)]
    public string? Title { get; set; }
    
    /// <summary>
    /// SubTitle
    /// </summary>
    [ImporterHeader(Name = "*SubTitle")]
    [ExporterHeader("*SubTitle", Format = "", Width = 25, IsBold = true)]
    public string SubTitle { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    [ImporterHeader(Name = "*Status")]
    [ExporterHeader("*Status", Format = "", Width = 25, IsBold = true)]
    public int? Status { get; set; }
    
    /// <summary>
    /// RequestPath
    /// </summary>
    [ImporterHeader(Name = "RequestPath")]
    [ExporterHeader("RequestPath", Format = "", Width = 25, IsBold = true)]
    public string? RequestPath { get; set; }
    
    /// <summary>
    /// RealPath
    /// </summary>
    [ImporterHeader(Name = "RealPath")]
    [ExporterHeader("RealPath", Format = "", Width = 25, IsBold = true)]
    public string? RealPath { get; set; }
    
    /// <summary>
    /// IsDynamic
    /// </summary>
    [ImporterHeader(Name = "*IsDynamic")]
    [ExporterHeader("*IsDynamic", Format = "", Width = 25, IsBold = true)]
    public bool? IsDynamic { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    [ImporterHeader(Name = "Description")]
    [ExporterHeader("Description", Format = "", Width = 25, IsBold = true)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Keywords
    /// </summary>
    [ImporterHeader(Name = "Keywords")]
    [ExporterHeader("Keywords", Format = "", Width = 25, IsBold = true)]
    public string? Keywords { get; set; }
    
    /// <summary>
    /// CanonicalUrl
    /// </summary>
    [ImporterHeader(Name = "CanonicalUrl")]
    [ExporterHeader("CanonicalUrl", Format = "", Width = 25, IsBold = true)]
    public string? CanonicalUrl { get; set; }
    
    /// <summary>
    /// Robots
    /// </summary>
    [ImporterHeader(Name = "Robots")]
    [ExporterHeader("Robots", Format = "", Width = 25, IsBold = true)]
    public string? Robots { get; set; }
    
    /// <summary>
    /// OgTitle
    /// </summary>
    [ImporterHeader(Name = "OgTitle")]
    [ExporterHeader("OgTitle", Format = "", Width = 25, IsBold = true)]
    public string? OgTitle { get; set; }
    
    /// <summary>
    /// OgImage
    /// </summary>
    [ImporterHeader(Name = "OgImage")]
    [ExporterHeader("OgImage", Format = "", Width = 25, IsBold = true)]
    public string? OgImage { get; set; }
    
    /// <summary>
    /// OgType
    /// </summary>
    [ImporterHeader(Name = "OgType")]
    [ExporterHeader("OgType", Format = "", Width = 25, IsBold = true)]
    public string? OgType { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    [ImporterHeader(Name = "DeleteTime")]
    [ExporterHeader("DeleteTime", Format = "", Width = 25, IsBold = true)]
    public DateTime? DeleteTime { get; set; }
    
}
