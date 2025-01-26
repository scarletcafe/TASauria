namespace ScarletCafe.TASauria;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;

[ExternalTool("TASauria")]
public sealed partial class TASauriaForm : ToolFormBase, IExternalToolForm {
    protected override string WindowTitleStatic
        => "TASauria";

    public TASauriaForm()
        => InitializeComponent(); // defined in TASauriaForm.Designer.cs
}
