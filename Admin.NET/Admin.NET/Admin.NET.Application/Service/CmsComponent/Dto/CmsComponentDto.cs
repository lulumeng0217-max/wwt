namespace Admin.NET.Application;

/// <summary>
/// component输出参数
/// </summary>
public class CmsComponentDto
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// PageId
    /// </summary>
    public long PageId { get; set; }
    
    /// <summary>
    /// ComponentTypeId
    /// </summary>
    public long ComponentTypeId { get; set; }
    
    /// <summary>
    /// Pid
    /// </summary>
    public long Pid { get; set; }
    
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
    public int SortOrder { get; set; }
    
    /// <summary>
    /// IsVisible
    /// </summary>
    public bool IsVisible { get; set; }
    
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


    public CmsComponentTypeDto CmsComponentType { get; set; }

    public List<CmsComponentDto> Children { get; set; } = new List<CmsComponentDto>();

}
