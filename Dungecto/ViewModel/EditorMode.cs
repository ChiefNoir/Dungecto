namespace Dungecto.ViewModel
{
    /// <summary>Mode for map editing</summary>
    public enum EditorMode
    {
        /// <summary>User can place/mode/edit tiles </summary>
        Tiler,

        /// <summary> User can fill grid with colors </summary>
        Filler,

        /// <summary> User can erase grid squares filled with color </summary>
        Eraser,

        /// <summary> User can pick color from filled section </summary>
        ColorPicker
    }
}
