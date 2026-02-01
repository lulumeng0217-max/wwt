using Admin.NET.Application.Enum;

namespace Admin.NET.Application;

/// <summary>
/// component_type输出参数
/// </summary>
public class CmsComponentTypeDto
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// ComponentKind
    /// </summary>
    public ComponentKindEnum ComponentKind { get; set; }
    
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
    public int Status { get; set; }
    
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
