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

            //Helper.LogGameObjectComponents(optionsOriginal);

            for (int i = 0; i < optionsOriginal.transform.childCount; i++)
            {
                var newGameObject = GameUtils.Instantiate(optionsOriginal.transform.GetChild(i).gameObject) as GameObject;
                if (newGameObject == null)
                    continue;
                newGameObject.SetActive(true);
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
            Logger.AppendLine("---------------------------------------");
            OptionsMenu.Get().Hide();
            Helper.LogGameObjectComponents(myOptions);
            for (int i = 0; i < myOptions.transform.childCount; i++)
            {
                var child = myOptions.transform.GetChild(i);
                if (child.gameObject.name.Contains("WindowContents"))
                {
                    Helper.LogGameObjectComponents(child.gameObject);
                    Logger.AppendLine("---------------------------------------");
                    for (int j = 0; j < child.childCount; j++)
                    {
                        var contentChild = child.GetChild(j).gameObject;
                        Helper.LogGameObjectComponents(contentChild);
                        if (contentChild.gameObject.name.Contains("MiddlePane"))
                        {
                            foreach (var slice in contentChild.gameObject.GetComponent<MultiSliceElement>().m_slices)
                            {
                                Logger.AppendLine($"slice: {slice.m_slice.name}");
                            }

                            contentChild.gameObject.GetComponent<MultiSliceElement>().m_slices.RemoveAt(0);
                            contentChild.gameObject.GetComponent<MultiSliceElement>().UpdateSlices();
                        }
                    }
                }
            }
            return EmptyTriggers;

        }
    }
}