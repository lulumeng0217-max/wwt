using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Entity;

/// <summary>
/// CMS page component data
/// </summary>
[SugarTable("CmsComponentData")]
public class CmsComponentData:EntityBase
{

    /// <summary>
    /// Component instance ID
    /// </summary>
    [SugarColumn]
    public long CmsComponentId { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    [SugarColumn(Length = 200)]
    public string? Title { get; set; }

    /// <summary>
    /// Subtitle
    /// </summary>
    [SugarColumn(Length = 200)]
    public string? Subtitle { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    [SugarColumn]
    public string? Content { get; set; }

    /// <summary>
    /// Link URL
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? LinkUrl { get; set; }

    /// <summary>
    /// Image URL
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Background color
    /// </summary>
    [SugarColumn(Length = 20)]
    public string? BgColor { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    [SugarColumn(Length = 100)]
    public string? Icon { get; set; }

    /// <summary>
    /// Custom properties (JSON)
    /// </summary>
    [SugarColumn(Length = 500)]
    public string? Props { get; set; }
}
