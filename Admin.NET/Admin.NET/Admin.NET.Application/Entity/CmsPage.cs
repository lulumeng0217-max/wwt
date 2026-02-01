using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Entity;

/// <summary>
/// CMS Page（N Page + Tempalte page）
/// </summary>
[SugarTable("CmsPage")]
public class CmsPage: EntityBaseTenantDel
{

    /// <summary>
    /// Page type: "template" or "page"
    /// </summary>
    [SugarColumn(Length = 50)]
    public string Pagetype { get; set; }
    [SugarColumn]
    public long TemplateId { get; set; }

    [SugarColumn]
    public long? Pid { get; set; }
    /// <summary>
    /// SEO title
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? Title { get; set; }

    /// <summary>
    /// SubeTitle
    /// </summary>
    [SugarColumn(Length = 255)]
    public string SubTitle { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [SugarColumn]
    public int Status { get; set; }

    /// <summary>
    /// Request path
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? RequestPath { get; set; }

    /// <summary>
    /// Real path
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? RealPath { get; set; }

    /// <summary>
    /// Whether the page is dynamic
    /// </summary>
    [SugarColumn]
    public bool IsDynamic { get; set; } = false;

 

    /// <summary>
    /// SEO description
    /// </summary>
    [SugarColumn]
    public string? Description { get; set; }

    /// <summary>
    /// Keywords
    /// </summary>
    [SugarColumn]
    public string? Keywords { get; set; }

    /// <summary>
    /// Canonical URL
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? CanonicalUrl { get; set; }

    /// <summary>
    /// Robots setting
    /// </summary>
    [SugarColumn(Length = 50)]
    public string? Robots { get; set; }

    /// <summary>
    /// Open Graph title
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? OgTitle { get; set; }

    /// <summary>
    /// Open Graph image URL
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? OgImage { get; set; }

    /// <summary>
    /// Open Graph type
    /// </summary>
    [SugarColumn(Length = 50)]
    public string? OgType { get; set; }
}
