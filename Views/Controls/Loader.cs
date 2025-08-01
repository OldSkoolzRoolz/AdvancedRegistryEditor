// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: Loader.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Windows.RegistryEditor.Views.Controls;
/// <summary>
///     Specifies the available loader animation styles.
/// </summary>
public enum LoaderKind
{

    /// <summary>
    /// </summary>
    Clock,

    /// <summary>
    /// </summary>
    Arrows,

    /// <summary>
    /// </summary>
    CircleBall,

    /// <summary>
    /// </summary>
    Loading,

    /// <summary>
    /// </summary>
    Snake,

    /// <summary>
    /// </summary>
    WheelThrobber

}


/// <summary>
///     Represents a custom UserControl that displays a loading animation and optionally disables other controls while
///     active.
/// </summary>
public partial class Loader : UserControl
{

    private LoaderKind loaderKind;






    /// <summary>
    ///     Initializes a new instance of the <see cref="Loader" /> class.
    /// </summary>
    public Loader()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
        InitializeComponent();
        DoubleBuffered = true;
        Visible = false;
        DisableControlsOnWork = true;
        SizeLoading = new Size(100, 100);
        LoaderKind = LoaderKind.Clock;
    }






    /// <summary>
    ///     Gets or sets a value indicating whether to disable all controls on the parent form when the loader is displayed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Description("Whether to disable all controls on form when loader is displayed.")]
    [Category("Behavior")]
    public bool DisableControlsOnWork { get; set; }

    /// <summary>
    ///     Gets or sets the size of the loader control when it is spinning.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Description("The size of the control when it's spinning. The purpose of this property is to allow you hiding your loader as a very small size in the corner, while being able to specify the expected size.")]
    [Category("Layout")]
    public Size SizeLoading { get; set; }

    /// <summary>
    ///     Gets or sets the loader animation style to be displayed.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Description("Gets or sets the loader appearance to be displayed.")]
    [Category("Appearance")]
    public LoaderKind LoaderKind
    {
        get => loaderKind;

        set
        {
            if (value == loaderKind)
            {
                return;
            }

            loaderKind = value;
            pbxGif.Image = CreateLoaderImage(value);
            if (DesignMode)
            {
                Invalidate();
            }
        }
    }

    /// <summary>
    ///     Gets the creation parameters for the control, enabling double-buffering for smoother rendering.
    /// </summary>
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x02000000; // Turns on WS_EX_COMPOSITED

            return cp;
        }
    }






    /// <summary>
    ///     Renders the background of the control to support transparency.
    /// </summary>
    /// <param name="control">The control to render.</param>
    /// <param name="graphics">The graphics context to draw on.</param>
    public static void Transparent(Control control, Graphics graphics)
    {
        Control owner = control.Parent;

        if (owner == null)
        {
            return;
        }

        Rectangle controlBounds = control.Bounds;
        ControlCollection ownerControls = owner.Controls;
        int controlIndex = ownerControls.IndexOf(control);
        Bitmap bitmapBehind = null;

        for (int i = controlIndex + 1; i < ownerControls.Count; i++)
        {
            Control targetControl = ownerControls[i];

            if (!targetControl.Bounds.IntersectsWith(controlBounds))
            {
                continue;
            }

            bitmapBehind ??= new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
            targetControl.DrawToBitmap(bitmapBehind, targetControl.Bounds);
        }

        if (bitmapBehind == null)
        {
            return;
        }

        graphics.DrawImage(bitmapBehind, control.ClientRectangle, controlBounds, GraphicsUnit.Pixel);
        bitmapBehind.Dispose();
    }






    /// <summary>
    ///     Creates the loader image for the specified <see cref="LoaderKind" />.
    /// </summary>
    /// <param name="value">The loader kind.</param>
    /// <returns>The corresponding loader image.</returns>
    private Image CreateLoaderImage(LoaderKind value)
    {
        return value switch
        {
            LoaderKind.Clock => (Image)Resources.loader_clock,

            //case LoaderKind.Arrows:
            //    return Resources.loader_arrows;
            //case LoaderKind.CircleBall:
            //    return Resources.loader_circleball;
            //case LoaderKind.Loading:
            //    return Resources.loader_loading;
            //case LoaderKind.Snake:
            //    return Resources.loader_snake;
            //case LoaderKind.WheelThrobber:
            //    return Resources.loader_wheelthrobber;
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }






    /// <summary>
    ///     Enables or disables all controls on the parent form except the loader itself.
    /// </summary>
    /// <param name="enabled">True to enable controls; false to disable.</param>
    private void EnableParentControls(bool enabled)
    {
        if (IsParentNull())
        {
            return;
        }

        foreach (Control control in ParentForm.Controls)
        {
            if (control.GetType() == typeof(Loader))
            {
                continue;
            }

            control.Invoke(new Action(() => control.Enabled = enabled));
        }
    }






    /// <summary>
    ///     Hides the loader and re-enables parent controls if necessary.
    /// </summary>
    public new void Hide()
    {
        if (IsParentNull() || !Visible)
        {
            return;
        }

        if (DisableControlsOnWork)
        {
            EnableParentControls(true);
        }

        Invoke(base.Hide);
        Invoke(SendToBack);
    }






    /// <summary>
    ///     Determines whether the parent form is null and logs a debug message if so.
    /// </summary>
    /// <param name="callerMethod">The calling method name (optional).</param>
    /// <returns>True if the parent form is null; otherwise, false.</returns>
    private bool IsParentNull([CallerMemberName] string callerMethod = "")
    {
        bool isNull = ParentForm == null;
        if (isNull)
        {
            Debug.WriteLine($"[{callerMethod}] - Parent cannot be null.");
        }

        return isNull;
    }






    /// <summary>
    ///     Paints the control, rendering its transparent background.
    /// </summary>
    /// <param name="e">The paint event arguments.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        Transparent(this, e.Graphics);
        base.OnPaint(e);
    }






    /// <summary>
    ///     Positions the loader in the center of its parent form.
    /// </summary>
    private void PositionToParent()
    {
        if (IsParentNull())
        {
            return;
        }

        Invoke(new Action(() => Left = (ParentForm.ClientSize.Width - Width) / 2));
        Invoke(new Action(() => Top = (ParentForm.ClientSize.Height - Height) / 2));
    }






    /// <summary>
    ///     Shows the loader, disables parent controls if necessary, and positions it in the center of the parent form.
    /// </summary>
    public new void Show()
    {
        if (IsParentNull() || Visible)
        {
            return;
        }

        Invoke(new Action(() => Size = SizeLoading));
        if (DisableControlsOnWork)
        {
            EnableParentControls(false);
        }

        PositionToParent();
        Invoke(base.Show);
        Invoke(BringToFront);
    }






    /// <summary>
    ///     Handles the load event for the loader, wiring up parent form size changes.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void Spinner_Load(object sender, EventArgs e)
    {
        if (IsParentNull())
        {
            return;
        }

        ParentForm.SizeChanged += delegate { PositionToParent(); };
    }

}