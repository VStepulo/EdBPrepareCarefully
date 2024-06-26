using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;

namespace EdB.PrepareCarefully {
    public class PawnLayerFaceTattoo : PawnLayer {
        private List<PawnLayerOption> options = new List<PawnLayerOption>();

        public override List<PawnLayerOption> Options {
            get {
                return options;
            }
            set {
                options = value;
            }
        }

        public override ColorSelectorType ColorSelectorType {
            get {
                return ColorSelectorType.None;
            }
        }

        public override bool IsOptionSelected(CustomizedPawn pawn, PawnLayerOption option) {
            if (!(option is PawnLayerOptionTattoo aOption)) {
                return false;
            }
            return pawn.Pawn.style.FaceTattoo == aOption.TattooDef;
        }

        public override int? GetSelectedIndex(CustomizedPawn pawn) {
            int selectedIndex = options.FirstIndexOf((option) => {
                PawnLayerOptionTattoo layerOption = option as PawnLayerOptionTattoo;
                if (layerOption == null) {
                    return false;
                }
                else {
                    return layerOption.TattooDef == pawn.Pawn.style.FaceTattoo;
                }
            });
            if (selectedIndex > -1) {
                return selectedIndex;
            }
            else {
                return null;
            }
        }

        public override PawnLayerOption GetSelectedOption(CustomizedPawn pawn) {
            int? selectedIndex = GetSelectedIndex(pawn);
            if (selectedIndex == null) {
                return null;
            }
            else if (selectedIndex.Value >= 0 && selectedIndex.Value < options.Count) {
                return options[selectedIndex.Value];
            }
            else {
                return null;
            }
        }

        public override void SelectOption(CustomizedPawn pawn, PawnLayerOption option) {
            if (option is PawnLayerOptionTattoo layerOption) {
                pawn.Pawn.style.FaceTattoo = layerOption.TattooDef;
                pawn.Pawn.Drawer?.renderer?.SetAllGraphicsDirty();
            }
        }


    }
}
