using Admin.NET.Application.Enum;
using Aop.Api.Domain;
using DocumentFormat.OpenXml.Wordprocessing;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Entity;


/// <summary>
/// CMS component type definition/management
/// </summary>
[SugarTable("CmsComponentType")]
public class CmsComponentType: EntityBaseDel
{

    /// <summary>
    /// Component name
    /// </summary>
    [SugarColumn(Length = 255)]
    public string Name { get; set; }


    /// <summary>
    /// ComponentKind
    /// </summary>
    [SugarColumn(Length = 1000)]
    public ComponentKindEnum ComponentKind { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [SugarColumn]
    public string? Description { get; set; }

    /// <summary>
    /// Default properties (JSON)
    /// </summary>
    [SugarColumn(IsJson = true, ColumnDataType = "jsonb")]
    public string? DefaultProps { get; set; }


    /// <summary>
    /// JSON schema
    /// </summary>
    [SugarColumn(IsJson = true, ColumnDataType = "jsonb")]
    public string? SetStyles { get; set; }

    /// <summary>
    /// JSON schema
    /// </summary>
    [SugarColumn(IsJson = true, ColumnDataType = "jsonb")]
    public string? Fields { get; set; }

    /// <summary>
    /// Status 10,20,30
    /// </summary>
    [SugarColumn]
    public int Status { get; set; } = 10;


    /// <summary>
    /// IsRemote
    /// </summary>    
    [SugarColumn(ColumnDescription = "IsRemote")]
    public bool IsRemote{ get; set; }


}
