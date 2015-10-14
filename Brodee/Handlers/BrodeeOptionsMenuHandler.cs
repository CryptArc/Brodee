using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class BrodeeOptionsMenuHandler : Handler
    {
        /*
        OptionsSection1 - Border around Graphics & Sound
        OptionsSection2 - Border around Preferences & Miscellaneous
        OptionsFrame - Background and MainBorder
        Header - Header Title
        MenuClickArea - Doesnt get cloned properly?
        InputBlocker - General input blocker, doesnt get cloned properly?
        WindowContents - 
        */
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            var optionsOriginal = OptionsMenu.Get().gameObject;

            var myOptions = new GameObject();
            myOptions.transform.localPosition = new Vector3(optionsOriginal.transform.position.x, optionsOriginal.transform.position.y, optionsOriginal.transform.position.z);
            myOptions.transform.localScale = new Vector3(optionsOriginal.transform.localScale.x, optionsOriginal.transform.localScale.y, optionsOriginal.transform.localScale.z);
            myOptions.transform.localRotation = new Quaternion(optionsOriginal.transform.localRotation.x, optionsOriginal.transform.localRotation.y, optionsOriginal.transform.localRotation.z, optionsOriginal.transform.localRotation.w);
            myOptions.transform.rotation = new Quaternion(optionsOriginal.transform.rotation.x, optionsOriginal.transform.rotation.y, optionsOriginal.transform.rotation.z, optionsOriginal.transform.rotation.w);
            myOptions.name = "My Clone OptionsClone";
            myOptions.transform.position = BaseUI.Get().GetOptionsMenuBone().position;
            myOptions.transform.SetParent(BaseUI.Get().transform);

            Logger.AppendLine($"optionsOriginal.transform.parent:{optionsOriginal.transform.parent.name}");

            //Helper.LogGameObjectComponents(optionsOriginal);

            for (int i = 0; i < optionsOriginal.transform.childCount; i++)
            {
                var newGameObject = GameUtils.Instantiate(optionsOriginal.transform.GetChild(i).gameObject) as GameObject;
                if (newGameObject == null)
                    continue;
                newGameObject.SetActive(true);
                newGameObject.layer = optionsOriginal.transform.GetChild(i).gameObject.layer;
                newGameObject.transform.SetParent(myOptions.transform);
                var origLocalPos = optionsOriginal.transform.GetChild(i).localPosition;
                var origLocalScale = optionsOriginal.transform.GetChild(i).localScale;
                var origLocalRotation = optionsOriginal.transform.GetChild(i).localRotation;
                var origRotation = optionsOriginal.transform.GetChild(i).rotation;
                newGameObject.transform.localPosition = new Vector3(origLocalPos.x, origLocalPos.y, origLocalPos.z);
                newGameObject.transform.localScale = new Vector3(origLocalScale.x, origLocalScale.y, origLocalScale.z);
                newGameObject.transform.localRotation = new Quaternion(origLocalRotation.x, origLocalRotation.y, origLocalRotation.z, origLocalRotation.w);
                newGameObject.transform.rotation = new Quaternion(origRotation.x, origRotation.y, origRotation.z, origRotation.w);
            }
            Logger.AppendLine("---------------------------------------");
            Logger.AppendLine("---------------------------");
            Logger.AppendLine("---------Original -----------");
            Helper.LogGameObjectComponents(optionsOriginal);
            var windowContents = myOptions.GetChildObjectContainingName("WindowContents");
            Helper.LogGameObjectComponents(windowContents);
            Logger.AppendLine("---------------------------------------");
            var middlePane = windowContents.GetChildObjectContainingName("MiddlePane");
            Helper.LogGameObjectComponents(middlePane);
            Logger.AppendLine("---------------------------------------");
            foreach (var slice in middlePane.GetComponent<MultiSliceElement>().m_slices)
            {
                Logger.AppendLine($"slice: {slice.m_slice.name}");

            }
            Logger.AppendLine("-----------Slice work---------");
            var secondSlice = middlePane.GetComponent<MultiSliceElement>().m_slices[1].m_slice;
            var creditsButton = secondSlice.GetChildObjectContainingName("CreditsButton");
            Logger.AppendLine("-----------Credits Button---------");
            Helper.LogGameObjectComponents(creditsButton);

            Logger.AppendLine("---------------------------------------");
            Logger.AppendLine("---------------------------");
            Logger.AppendLine("---------Clone -----------");
            Helper.LogGameObjectComponents(myOptions);
            windowContents = myOptions.GetChildObjectContainingName("WindowContents");
            Helper.LogGameObjectComponents(windowContents);
            Logger.AppendLine("---------------------------------------");
            middlePane = windowContents.GetChildObjectContainingName("MiddlePane");
            Helper.LogGameObjectComponents(middlePane);
            Logger.AppendLine("---------------------------------------");
            foreach (var slice in middlePane.GetComponent<MultiSliceElement>().m_slices)
            {
                Logger.AppendLine($"slice: {slice.m_slice.name}");

            }
            Logger.AppendLine("-----------Slice work---------");
            secondSlice = middlePane.GetComponent<MultiSliceElement>().m_slices[1].m_slice;
            creditsButton = secondSlice.GetChildObjectContainingName("CreditsButton");
            Logger.AppendLine("-----------Credits Button---------");
            Helper.LogGameObjectComponents(creditsButton);

            var midPane = windowContents.GetChildObjectContainingName("MiddlePane");
            midPane.GetComponent<MultiSliceElement>().UpdateSlices();
            var leftPane = windowContents.GetChildObjectContainingName("LeftPane");
            leftPane.GetComponent<MultiSliceElement>().UpdateSlices();
            var rightPane = windowContents.GetChildObjectContainingName("RightPane");
            rightPane.GetComponent<MultiSliceElement>().UpdateSlices();

            return EmptyTriggers;

        }
    }
}