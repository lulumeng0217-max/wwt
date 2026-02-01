// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Admin.NET.Application.Enum;

namespace Admin.NET.Application;

/// <summary>
/// component_type基础输入参数
/// </summary>
public class CmsComponentTypeBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// Name
    /// </summary>
    [Required(ErrorMessage = "Name不能为空")]
    public virtual string Name { get; set; }
    
    /// <summary>
    /// ComponentKind
    /// </summary>
    [Dict(nameof(ComponentKindEnum), AllowNullValue=true)]
    [Required(ErrorMessage = "ComponentKind不能为空")]
    public virtual ComponentKindEnum? ComponentKind { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public virtual string? Description { get; set; }
    
    /// <summary>
    /// DefaultProps
    /// </summary>
    public virtual string? DefaultProps { get; set; }
    
    /// <summary>
    /// SetStyles
    /// </summary>
    public virtual string? SetStyles { get; set; }
    
    /// <summary>
    /// Fields
    /// </summary>
    public virtual string? Fields { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    [Required(ErrorMessage = "Status不能为空")]
    public virtual int? Status { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    public virtual DateTime? DeleteTime { get; set; }
    
}

/// <summary>
/// component_type分页查询输入参数
/// </summary>
public class PageCmsComponentTypeInput : BasePageInput
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// ComponentKind
    /// </summary>
    [Dict(nameof(ComponentKindEnum), AllowNullValue=true)]
    public ComponentKindEnum? ComponentKind { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// DefaultProps
    /// </summary>
    public string? DefaultProps { get; set; }
    
    /// <summary>
    /// SetStyles
    /// </summary>
    public string? SetStyles { get; set; }
    
    /// <summary>
    /// Fields
    /// </summary>
    public string? Fields { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    public int? Status { get; set; }
    
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
/// component_type增加输入参数
/// </summary>
public class AddCmsComponentTypeInput
{
    /// <summary>
    /// Name
    /// </summary>
    [Required(ErrorMessage = "Name不能为空")]
    [MaxLength(255, ErrorMessage = "Name字符长度不能超过255")]
    public string Name { get; set; }
    
    /// <summary>
    /// ComponentKind
    /// </summary>
    [Dict(nameof(ComponentKindEnum), AllowNullValue=true)]
    [Required(ErrorMessage = "ComponentKind不能为空")]
    public ComponentKindEnum? ComponentKind { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    [MaxLength(255, ErrorMessage = "Description字符长度不能超过255")]
    public string? Description { get; set; }
    
    /// <summary>
    /// DefaultProps
    /// </summary>
    public string? DefaultProps { get; set; }
    
    /// <summary>
    /// SetStyles
    /// </summary>
    public string? SetStyles { get; set; }
    
    /// <summary>
    /// Fields
    /// </summary>
    public string? Fields { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    [Required(ErrorMessage = "Status不能为空")]
    public int? Status { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    public DateTime? DeleteTime { get; set; }
    
}

/// <summary>
/// component_type删除输入参数
/// </summary>
public class DeleteCmsComponentTypeInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// component_type更新输入参数
/// </summary>
public class UpdateCmsComponentTypeInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// Name
    /// </summary>    
    [Required(ErrorMessage = "Name不能为空")]
    [MaxLength(255, ErrorMessage = "Name字符长度不能超过255")]
    public string Name { get; set; }
    
    /// <summary>
    /// ComponentKind
    /// </summary>    
    [Dict(nameof(ComponentKindEnum), AllowNullValue=true)]
    [Required(ErrorMessage = "ComponentKind不能为空")]
    public ComponentKindEnum? ComponentKind { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>    
    public string? Description { get; set; }
    
    /// <summary>
    /// DefaultProps
    /// </summary>    
    public string? DefaultProps { get; set; }
    
    /// <summary>
    /// SetStyles
    /// </summary>    
    public string? SetStyles { get; set; }
    
    /// <summary>
    /// Fields
    /// </summary>    
    public string? Fields { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>    
    [Required(ErrorMessage = "Status不能为空")]
    public int? Status { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>    
    public DateTime? DeleteTime { get; set; }
    
}

/// <summary>
/// component_type主键查询输入参数
/// </summary>
public class QueryByIdCmsComponentTypeInput : DeleteCmsComponentTypeInput
{
}

/// <summary>
/// component_type数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportCmsComponentTypeInput : BaseImportInput
{
    /// <summary>
    /// Name
    /// </summary>
    [ImporterHeader(Name = "*Name")]
    [ExporterHeader("*Name", Format = "", Width = 25, IsBold = true)]
    public string Name { get; set; }
    
    /// <summary>
    /// ComponentKind
    /// </summary>
    [ImporterHeader(Name = "*ComponentKind")]
    [ExporterHeader("*ComponentKind", Format = "", Width = 25, IsBold = true)]
    public ComponentKindEnum? ComponentKind { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    [ImporterHeader(Name = "Description")]
    [ExporterHeader("Description", Format = "", Width = 25, IsBold = true)]
    public string? Description { get; set; }
    
    /// <summary>
    /// DefaultProps
    /// </summary>
    [ImporterHeader(Name = "DefaultProps")]
    [ExporterHeader("DefaultProps", Format = "", Width = 25, IsBold = true)]
    public string? DefaultProps { get; set; }
    
    /// <summary>
    /// SetStyles
    /// </summary>
    [ImporterHeader(Name = "SetStyles")]
    [ExporterHeader("SetStyles", Format = "", Width = 25, IsBold = true)]
    public string? SetStyles { get; set; }
    
    /// <summary>
    /// Fields
    /// </summary>
    [ImporterHeader(Name = "Fields")]
    [ExporterHeader("Fields", Format = "", Width = 25, IsBold = true)]
    public string? Fields { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    [ImporterHeader(Name = "*Status")]
    [ExporterHeader("*Status", Format = "", Width = 25, IsBold = true)]
    public int? Status { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    [ImporterHeader(Name = "DeleteTime")]
    [ExporterHeader("DeleteTime", Format = "", Width = 25, IsBold = true)]
    public DateTime? DeleteTime { get; set; }
    
}
