﻿using CSharpMath.Atoms;
using CSharpMath.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CSharpMath.Display {
  public class TextLineDisplay : IDisplay {
    public TextLineDisplay(List<TextRunDisplay> runs,
      IEnumerable<IMathAtom> atoms) {
      Runs = runs;
      Atoms = atoms.ToList();
    }
    public List<TextRunDisplay> Runs { get; }
    public List<IMathAtom> Atoms { get; }
    public string Text {
      get {
        string r = "";
        foreach (var run in Runs) {
          r += run.Run.Text;
        }
        return r;
      }
    }

    public RectangleF DisplayBounds
      => this.ComputeDisplayBounds();

    public void Draw(IGraphicsContext context) {

    }
    public PointF Position { get; set; }

    public float Ascent => Runs.CollectionAscent();
    public float Descent => Runs.CollectionDescent(); // TODO: runs probably need a location.
    public float Width => Runs.CollectionWidth();
    public Range Range => RangeExtensions.Combine(Runs.Select(r => r.Range));
    public bool HasScript { get; set; }
  }
}

