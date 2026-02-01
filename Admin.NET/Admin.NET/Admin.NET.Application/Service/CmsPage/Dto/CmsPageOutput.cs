using Magicodes.ExporterAndImporter.Core;
namespace Admin.NET.Application;

/// <summary>
/// page输出参数
/// </summary>
public class CmsPageOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }    
    
    /// <summary>
    /// Pagetype
    /// </summary>
    public string Pagetype { get; set; }
    public long Pid { get; set; }

    /// <summary>
    /// TemplateId
    /// </summary>
    public long TemplateId { get; set; }    
    
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
    public int Status { get; set; }    
    
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
    public bool IsDynamic { get; set; }    
    
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
    /// TenantId
    /// </summary>
    public long? TenantId { get; set; }    
    
    /// <summary>
    /// IsDelete
    /// </summary>
    public bool IsDelete { get; set; }    
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    public DateTime? DeleteTime { get; set; }    
    
    /// <summary>
    /// CreateTime
    /// </summary>
    public DateTime? CreateTime { get; set; }    
    
    /// <summary>
    /// UpdateTime
    /// </summary>
    public DateTime? UpdateTime { get; set; }    
    
    /// <summary>
    /// CreateUserId
    /// </summary>
    public long? CreateUserId { get; set; }    
    
    /// <summary>
    /// CreateUserName
    /// </summary>
    public string? CreateUserName { get; set; }    
    
    /// <summary>
    /// UpdateUserId
    /// </summary>
    public long? UpdateUserId { get; set; }    
    
    /// <summary>
    /// UpdateUserName
    /// </summary>
    public string? UpdateUserName { get; set; }    
    
}

/// <summary>
/// page数据导入模板实体
/// </summary>
public class ExportCmsPageOutput : ImportCmsPageInput
{
    [ImporterHeader(IsIgnore = true)]
    [ExporterHeader(IsIgnore = true)]
    public override string Error { get; set; }
}
