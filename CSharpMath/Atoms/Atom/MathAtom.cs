﻿using CSharpMath.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpMath.Atoms {
  public class MathAtom : IMathAtom {

    public virtual string StringValue {
      get {
        StringBuilder builder = new StringBuilder(Nucleus);
        builder.AppendInBraces(Superscript, NullHandling.EmptyString);
        builder.AppendInBraces(Subscript, NullHandling.EmptyString);
        return builder.ToString();
      }
    }
    public MathAtomType AtomType { get; set; }
    public string Nucleus { get; set; }
    private IMathList _Superscript;
    public IMathList Superscript {
      get => _Superscript;
      set {
        if (ScriptsAllowed || value == null) {
          _Superscript = value;
        } else {
          throw new Exception("Scripts are not allowed in itom type " + AtomType.ToText());
        }
      }
    }
    private IMathList _Subscript;
    public IMathList Subscript {
      get => _Subscript;
      set {
        if (ScriptsAllowed || value == null) {
          _Subscript = value;
        } else {
          throw new Exception("Scripts are not allowed in itom type " + AtomType.ToText());
        }
      }
    }
    public FontStyle FontStyle { get; set; }

    public Range IndexRange { get; set; }

    public List<IMathAtom> FusedAtoms { get; set; }

    public bool ScriptsAllowed => AtomType < MathAtomType.Boundary;

    public MathAtom(MathAtomType type, string nucleus) {
      AtomType = type;
      Nucleus = nucleus;
    }
    public MathAtom(MathAtom cloneMe, bool finalize) {
      AtomType = cloneMe.AtomType;
      Nucleus = cloneMe.Nucleus;
      Superscript = AtomCloner.Clone(cloneMe.Superscript, finalize);
      Subscript = AtomCloner.Clone(cloneMe.Subscript, finalize);
      IndexRange = cloneMe.IndexRange;
      FontStyle = cloneMe.FontStyle;
    }

    public void Fuse(IMathAtom otherAtom) {
      if (Subscript != null) {
        throw new InvalidOperationException("Cannot fuse into an atom with a subscript " + StringValue);
      }
      if (Superscript != null) {
        throw new InvalidOperationException("Cannot fuse into an atom with a superscript " + StringValue);
      }
      if (otherAtom.AtomType != AtomType) {
        throw new InvalidOperationException("Cannot fuse atoms with different types");
      }
      if (FusedAtoms == null) {
        FusedAtoms = new List<IMathAtom> {
          AtomCloner.Clone(this, false)
        };
      }
      if (otherAtom.FusedAtoms != null) {
        FusedAtoms.AddRange(otherAtom.FusedAtoms);
      } else {
        FusedAtoms.Add(otherAtom);
      }
      Nucleus += otherAtom.Nucleus;
      IndexRange = new Range(IndexRange.Location, IndexRange.Length + otherAtom.IndexRange.Length);
      Subscript = otherAtom.Subscript;
      Superscript = otherAtom.Superscript;
    }

    public virtual T Accept<T, THelper>(IMathAtomVisitor<T, THelper> visitor, THelper helper)
      => visitor.Visit(this, helper);

    public override string ToString() =>
      AtomType.ToText() + " " + StringValue;

    public bool EqualsAtom(MathAtom otherAtom) {
      var r = (otherAtom != null);
      r &= Nucleus == otherAtom.Nucleus;
      r &= AtomType == otherAtom.AtomType;
      r &= Superscript.NullCheckingEquals(otherAtom.Superscript);
      r &= Subscript.NullCheckingEquals(otherAtom.Subscript);
      r &= IndexRange == otherAtom.IndexRange;
      r &= FontStyle == otherAtom.FontStyle;
      r &= otherAtom.GetType() == this.GetType();
      return r;
    }
    

    public override bool Equals (object obj) {
      if (obj is MathAtom) {
        return this.EqualsAtom((MathAtom)obj);
      }
      return false;
    }


    public override int GetHashCode() {
      unchecked {
        return
        AtomType.GetHashCode()
        + 3 * ((Superscript == null) ? 0 : Superscript.GetHashCode())
        + 5 * ((Subscript == null) ? 0 : Subscript.GetHashCode())
        + 7 * IndexRange.GetHashCode()
        + 13 * FontStyle.GetHashCode();
      }
    }
  }
}