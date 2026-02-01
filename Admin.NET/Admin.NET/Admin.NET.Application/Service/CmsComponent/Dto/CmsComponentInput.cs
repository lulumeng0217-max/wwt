using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Admin.NET.Application;

/// <summary>
/// component基础输入参数
/// </summary>
public class CmsComponentBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// PageId
    /// </summary>
    [Required(ErrorMessage = "PageId不能为空")]
    public virtual long? PageId { get; set; }
    
    /// <summary>
    /// ComponentTypeId
    /// </summary>
    [Required(ErrorMessage = "ComponentTypeId不能为空")]
    public virtual long? ComponentTypeId { get; set; }
    
    /// <summary>
    /// Pid
    /// </summary>
    [Required(ErrorMessage = "Pid不能为空")]
    public virtual long? Pid { get; set; }
    
    /// <summary>
    /// Props
    /// </summary>
    [Required(ErrorMessage = "Props不能为空")]
    public virtual string Props { get; set; }
    
    /// <summary>
    /// Styles
    /// </summary>
    public virtual string? Styles { get; set; }
    
    /// <summary>
    /// SortOrder
    /// </summary>
    [Required(ErrorMessage = "SortOrder不能为空")]
    public virtual int? SortOrder { get; set; }
    
    /// <summary>
    /// IsVisible
    /// </summary>
    [Required(ErrorMessage = "IsVisible不能为空")]
    public virtual bool? IsVisible { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    public virtual DateTime? DeleteTime { get; set; }
    
}

/// <summary>
/// component分页查询输入参数
/// </summary>
public class PageCmsComponentInput : BasePageInput
{
    /// <summary>
    /// PageId
    /// </summary>
    public long? PageId { get; set; }
    
    /// <summary>
    /// ComponentTypeId
    /// </summary>
    public long? ComponentTypeId { get; set; }
    
    /// <summary>
    /// Pid
    /// </summary>
    public long? Pid { get; set; }
    
    /// <summary>
    /// Props
    /// </summary>
    public string Props { get; set; }
    
    /// <summary>
    /// Styles
    /// </summary>
    public string? Styles { get; set; }
    
    /// <summary>
    /// SortOrder
    /// </summary>
    public int? SortOrder { get; set; }
    
    /// <summary>
    /// IsVisible
    /// </summary>
    public bool? IsVisible { get; set; }
    
    /// <summary>
    /// DeleteTime范围
    /// </summary>
     public DateTime?[] DeleteTimeRange { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
}
public class AddCmsComponentBatchInput
{
    /// <summary>
    /// PageId
    /// </summary>
    public long? PageId { get; set; }
    /// <summary>
    /// 组件列表
    /// </summary>
    public List<AddBatchCmsComponentInput> ComponentList { get; set; }
}

/// <summary>
/// component add string的
/// </summary>
public class AddBatchCmsComponentInput
{
    public string Id { get;set; }
    /// <summary>
    /// PageId
    /// </summary>
    public string? Pid{ get; set; }

    /// <summary>
    /// ComponentTypeId
    /// </summary>
    [Required(ErrorMessage = "ComponentTypeId不能为空")]
    public long? ComponentTypeId { get; set; }



    /// <summary>
    /// Props
    /// </summary>
    [Required(ErrorMessage = "Props不能为空")]
    public string Props { get; set; }

    /// <summary>
    /// Styles
    /// </summary>
    public string? Styles { get; set; }

    /// <summary>
    /// SortOrder
    /// </summary>
    [Required(ErrorMessage = "SortOrder不能为空")]
    public int? SortOrder { get; set; }

    /// <summary>
    /// IsVisible
    /// </summary>
    [Required(ErrorMessage = "IsVisible不能为空")]
    public bool? IsVisible { get; set; }

    /// <summary>
    /// DeleteTime
    /// </summary>
    public DateTime? DeleteTime { get; set; }

}
/// <summary>
/// component增加输入参数
/// </summary>
public class AddCmsComponentInput
{
    /// <summary>
    /// PageId
    /// </summary>
    public long? PageId { get; set; }
    
    /// <summary>
    /// ComponentTypeId
    /// </summary>
    [Required(ErrorMessage = "ComponentTypeId不能为空")]
    public long? ComponentTypeId { get; set; }

    /// <summary>
    /// Pid
    /// </summary>
    public long? Pid { get; set; } = 0;
    
    /// <summary>
    /// Props
    /// </summary>
    [Required(ErrorMessage = "Props不能为空")]
    public string Props { get; set; }
    
    /// <summary>
    /// Styles
    /// </summary>
    public string? Styles { get; set; }
    
    /// <summary>
    /// SortOrder
    /// </summary>
    [Required(ErrorMessage = "SortOrder不能为空")]
    public int? SortOrder { get; set; }
    
    /// <summary>
    /// IsVisible
    /// </summary>
    [Required(ErrorMessage = "IsVisible不能为空")]
    public bool? IsVisible { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    public DateTime? DeleteTime { get; set; }
    
}

/// <summary>
/// component删除输入参数
/// </summary>
public class DeleteCmsComponentInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// component更新输入参数
/// </summary>
public class UpdateCmsComponentInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// PageId
    /// </summary>    
    [Required(ErrorMessage = "PageId不能为空")]
    public long? PageId { get; set; }
    
    /// <summary>
    /// ComponentTypeId
    /// </summary>    
    [Required(ErrorMessage = "ComponentTypeId不能为空")]
    public long? ComponentTypeId { get; set; }
    
    /// <summary>
    /// Pid
    /// </summary>    
    [Required(ErrorMessage = "Pid不能为空")]
    public long? Pid { get; set; }
    
    /// <summary>
    /// Props
    /// </summary>    
    [Required(ErrorMessage = "Props不能为空")]
    [MaxLength(1000, ErrorMessage = "Props字符长度不能超过1000")]
    public string Props { get; set; }
    
    /// <summary>
    /// Styles
    /// </summary>    
    [MaxLength(500, ErrorMessage = "Styles字符长度不能超过500")]
    public string? Styles { get; set; }
    
    /// <summary>
    /// SortOrder
    /// </summary>    
    [Required(ErrorMessage = "SortOrder不能为空")]
    public int? SortOrder { get; set; }
    
    /// <summary>
    /// IsVisible
    /// </summary>    
    [Required(ErrorMessage = "IsVisible不能为空")]
    public bool? IsVisible { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>    
    public DateTime? DeleteTime { get; set; }
    
}

/// <summary>
/// component主键查询输入参数
/// </summary>
public class QueryByIdCmsComponentInput : DeleteCmsComponentInput
{
}

/// <summary>
/// component数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportCmsComponentInput : BaseImportInput
{
    /// <summary>
    /// PageId
    /// </summary>
    [ImporterHeader(Name = "*PageId")]
    [ExporterHeader("*PageId", Format = "", Width = 25, IsBold = true)]
    public long? PageId { get; set; }
    
    /// <summary>
    /// ComponentTypeId
    /// </summary>
    [ImporterHeader(Name = "*ComponentTypeId")]
    [ExporterHeader("*ComponentTypeId", Format = "", Width = 25, IsBold = true)]
    public long? ComponentTypeId { get; set; }
    
    /// <summary>
    /// Pid
    /// </summary>
    [ImporterHeader(Name = "*Pid")]
    [ExporterHeader("*Pid", Format = "", Width = 25, IsBold = true)]
    public long? Pid { get; set; }
    
    /// <summary>
    /// Props
    /// </summary>
    [ImporterHeader(Name = "*Props")]
    [ExporterHeader("*Props", Format = "", Width = 25, IsBold = true)]
    public string Props { get; set; }
    
    /// <summary>
    /// Styles
    /// </summary>
    [ImporterHeader(Name = "Styles")]
    [ExporterHeader("Styles", Format = "", Width = 25, IsBold = true)]
    public string? Styles { get; set; }
    
    /// <summary>
    /// SortOrder
    /// </summary>
    [ImporterHeader(Name = "*SortOrder")]
    [ExporterHeader("*SortOrder", Format = "", Width = 25, IsBold = true)]
    public int? SortOrder { get; set; }
    
    /// <summary>
    /// IsVisible
    /// </summary>
    [ImporterHeader(Name = "*IsVisible")]
    [ExporterHeader("*IsVisible", Format = "", Width = 25, IsBold = true)]
    public bool? IsVisible { get; set; }
    
    /// <summary>
    /// DeleteTime
    /// </summary>
    [ImporterHeader(Name = "DeleteTime")]
    [ExporterHeader("DeleteTime", Format = "", Width = 25, IsBold = true)]
    public DateTime? DeleteTime { get; set; }
    
}
