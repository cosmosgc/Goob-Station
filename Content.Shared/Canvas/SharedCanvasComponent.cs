using Content.Shared.Canvas;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using System;

namespace Content.Shared.Canvas;

[NetworkedComponent, ComponentProtoName("Canvas"), Access(typeof(SharedCanvasSystem))]
public abstract partial class SharedCanvasComponent : Component
{
    private string _selectedState = string.Empty;
    public string SelectedState
    {
        get => _selectedState;
        set
        {
            if (_selectedState == value)
                return;

            _selectedState = value;
            Dirty();
        }
    }

    [DataField("color")]
    private Color _color;
    public Color Color
    {
        get => _color;
        set
        {
            if (_color == value)
                return;

            _color = value;
            Dirty();
        }
    }

    [DataField("paintingCode")]
    private string _paintingCode = string.Empty;
    public string PaintingCode
    {
        get => _paintingCode;
        set
        {
            if (_paintingCode == value)
                return;

            _paintingCode = value;
            Dirty();
        }
    }
    [DataField("artist")]
    private string _artist = string.Empty;
    public string Artist
    {
        get => _artist;
        set
        {
            if (_artist == value)
                return;

            _artist = value;
            Dirty();
        }
    }

    [DataField("signature")]
    private string _signature = string.Empty;
    public string Signature
    {
        get => _signature;
        set
        {
            if (_signature == value)
                return;

            _signature = value;
            Dirty();
        }
    }

    [DataField("height")]
    private int _height = 16;
    public int Height
    {
        get => _height;
        set
        {
            if (_height == value)
                return;

            _height = value;
            Dirty();
        }
    }

    [DataField("width")]
    private int _width = 16;
    public int Width
    {
        get => _width;
        set
        {
            if (_width == value)
                return;

            _width = value;
            //Nwidth = value;
            Dirty();
        }
    }

    [DataField("size")]
    private int _sizeMultiplier = 2;
    public int SizeMultiplier
    {
        get => _sizeMultiplier;
        set
        {
            if (_sizeMultiplier == value)
                return;

            _sizeMultiplier = value;
            Dirty();
        }
    }

    public bool SelectableColor { get; internal set; }


    /// <summary>
    /// Key to the Canvas UI
    /// </summary>
    [Serializable, NetSerializable]
    public enum CanvasUiKey : byte
    {
        Key,
    }

    /// <summary>
    /// Used by the client to notify the server about the selected decal ID
    /// </summary>
    [Serializable, NetSerializable]
    public sealed class CanvasSelectMessage : BoundUserInterfaceMessage
    {
        public readonly string State;
        public CanvasSelectMessage(string selected)
        {
            State = selected;
        }
    }

    /// <summary>
    /// Used by the client to notify the server about finalizing the painting
    /// </summary>
    [Serializable, NetSerializable]
    public sealed class CanvasFinalizeMessage : BoundUserInterfaceMessage
    {
        public readonly string State;
        public CanvasFinalizeMessage(string selected)
        {
            State = selected;
        }
    }

    [Serializable, NetSerializable]
    public sealed class CanvasSignatureMessage : BoundUserInterfaceMessage
    {
        public readonly string Signature;
        public CanvasSignatureMessage(string signature)
        {
            Signature = signature;
        }
    }

    [Serializable, NetSerializable]
    public sealed class CanvasHeightMessage : BoundUserInterfaceMessage
    {
        public readonly int Height;
        public CanvasHeightMessage(int height)
        {
            Height = height;
        }
    }

    [Serializable, NetSerializable]
    public sealed class CanvasWidthMessage : BoundUserInterfaceMessage
    {
        public readonly int Width;
        public CanvasWidthMessage(int width)
        {
            Width = width;
        }
    }

    /// <summary>
    /// Sets the color of the Canvas, used by Rainbow Canvas
    /// </summary>
    [Serializable, NetSerializable]
    public sealed class CanvasColorMessage : BoundUserInterfaceMessage
    {
        public readonly Color Color;
        public CanvasColorMessage(Color color)
        {
            Color = color;
        }
    }

    /// <summary>
    /// Server to CLIENT. Notifies the BUI that a decal with given ID has been drawn.
    /// Allows the client UI to advance forward in the client-only ephemeral queue,
    /// preventing the Canvas from becoming a magic text storage device.
    /// </summary>
    [Serializable, NetSerializable]
    public sealed class CanvasUsedMessage : BoundUserInterfaceMessage
    {
        public readonly string DrawnDecal;

        public CanvasUsedMessage(string drawn)
        {
            DrawnDecal = drawn;
        }
    }

    /// <summary>
    /// Component state
    /// </summary>
    [Serializable, NetSerializable]
    public sealed class CanvasComponentState : ComponentState
    {
        public readonly Color Color;
        public readonly string State;
        public readonly string PaintingCode;
        public readonly int Height;
        public readonly int Width;
        public readonly string Artist;
        public readonly string Signature;
        public readonly int SizeMultiplier;

        public CanvasComponentState(Color color, string state, string paintingCode, int height, int width, string artist, int sizeMultiplier, string signature)
        {
            Color = color;
            State = state;
            PaintingCode = paintingCode;
            Height = height;
            Width = width;
            Artist = artist;
            SizeMultiplier = sizeMultiplier;
            Signature = signature;
        }
    }

    /// <summary>
    /// The state of the Canvas UI as sent by the server
    /// </summary>
    [Serializable, NetSerializable]
    public sealed class CanvasBoundUserInterfaceState : BoundUserInterfaceState
    {
        public string Selected;
        public string PaintingCode;
        /// <summary>
        /// Whether or not the color can be selected
        /// </summary>
        public bool SelectableColor;
        public Color Color;
        public int Height;
        public int Width;
        public string Artist;
        public string Signature;

        public CanvasBoundUserInterfaceState(string selected, string paintingCode, Color color, int height, int width, string artist, string signature)
        {
            PaintingCode = paintingCode;
            Selected = selected;
            Color = color;
            Height = height;
            Width = width;
            Artist = artist;
            Signature = signature;
        }
    }
}



