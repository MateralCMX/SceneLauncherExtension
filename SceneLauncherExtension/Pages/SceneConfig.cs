namespace SceneLauncherExtension.Pages
{
    internal class SceneConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "场景1";
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 程序路径
        /// </summary>
        public string[] ProgramPaths { get; set; } = [];
    }
}
