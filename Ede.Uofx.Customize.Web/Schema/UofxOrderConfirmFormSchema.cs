//----------------------
// <auto-generated>
//    請勿修改此檔案內容，以免產生不可預期的錯誤。
//    產生日期:2023-08-25 14:03:04。
//    類別:作業, 表單:訂單確認單, 版本:1.0
//     Generated using the NJsonSchema v10.7.1.0 (Newtonsoft.Json v9.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------



//using System;
//using System.Collections.Generic;
//using Ede.Uofx.PubApi.Sdk.NetStd.Interfaces;

namespace Ede.Uofx.FormSchema.UofxFormSchema
{
    #pragma warning disable // Disable all warnings

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.7.1.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class FileItem
    {
        /// <summary>
        /// Id
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("FileName", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string FileName { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.7.1.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class UofxFormSchemaFields
    {
        /// <summary>
        /// 客戶代號
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C002", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string C002 { get; set; }

        /// <summary>
        /// 訂單單號
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C003", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string C003 { get; set; }

        /// <summary>
        /// 預計採購金額
        /// </summary>
        [Newtonsoft.Json.JsonProperty("C004", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.Range(1, 999)]
        public int? C004 { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.7.1.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class UofxFormSchema
    {
        /// <summary>
        /// 請勿修改
        /// </summary>
        [Newtonsoft.Json.JsonProperty("FormId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid FormId { get; set; } = new System.Guid("18ccff76-17d3-44ab-014c-08dba52de688");

        /// <summary>
        /// 請勿修改
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ScriptId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid ScriptId { get; set; } = new System.Guid("37446367-b81e-470e-0246-08dba52de688");

        /// <summary>
        /// 人員帳號
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Account", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Account { get; set; }

        /// <summary>
        /// 申請者原公司代碼(如為其他公司兼職才需要填)
        /// </summary>
        [Newtonsoft.Json.JsonProperty("CorpCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CorpCode { get; set; }

        /// <summary>
        /// 部門代碼，此人員所屬的部門
        /// </summary>
        [Newtonsoft.Json.JsonProperty("DeptCode", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 起單完成要回覆的 api url
        /// </summary>
        [Newtonsoft.Json.JsonProperty("CallBackUrl", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CallBackUrl { get; set; }

        /// <summary>
        /// 使用者自訂資料，會在 CallBack 時回傳
        /// </summary>
        [Newtonsoft.Json.JsonProperty("CustomData", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CustomData { get; set; }

        /// <summary>
        /// 急件
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Urgent", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Urgent { get; set; }

        /// <summary>
        /// 備註說明
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Opinion", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Opinion { get; set; }

        /// <summary>
        /// 附件。請先呼叫檔案上傳 (UofxService.File.FileUpload) 取得 id 和 name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("AttachFiles", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<FileItem> AttachFiles { get; set; }

        /// <summary>
        /// 表單欄位
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Fields", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public UofxFormSchemaFields Fields { get; set; } = new UofxFormSchemaFields();


    }
}
