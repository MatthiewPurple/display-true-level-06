using MelonLoader;
using HarmonyLib;
using Il2Cpp;
using display_true_level_06;
using Il2Cppnewdata_H;

[assembly: MelonInfo(typeof(DisplayTrueLevel06), "Display true level (0.6 ver.)", "1.0.0", "Matthiew Purple")]
[assembly: MelonGame("アトラス", "smt3hd")]

namespace display_true_level_06;
public class DisplayTrueLevel06 : MelonMod
{
    // After capping the level for display
    [HarmonyPatch(typeof(datCalc), nameof(datCalc.datGetLevelForDraw))]
    private class Patch
    {
        public static void Postfix(ref datUnitWork_t work, ref int __result)
        {
            __result = work.level; // Displays the actual level (on two digits)

            // Removes the first digit if it's 0 for aesthetic purposes
            while (__result > 99)
            {
                __result -= 100;
            }
        }
    }

    // After drawing the status panel
    [HarmonyPatch(typeof(cmpDrawStatus), nameof(cmpDrawStatus.cmpDrawStatusPanel))]
    private class Patch2
    {
        public static void Postfix(ref datUnitWork_t pStock)
        {
            // Changes the color every 100 levels and updates the displayed object
            if (pStock.level > 99)
            {
                cmpStatus._statusUIScr.transform.Find("sinfo_basic/slvnum").gameObject.GetComponent<CounterCtr>().colorIndex = pStock.level / 100 + 1;
                cmpStatus._statusUIScr.transform.Find("sinfo_basic/slvnum").gameObject.GetComponent<CounterCtr>().Change();
            }
        }
    }
}
