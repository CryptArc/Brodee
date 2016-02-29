using System;
using System.Collections.Generic;

namespace Brodee.Core.Handlers
{
    public static class CardStuff
    {
        private static Dictionary<object, SpellStateType> _dictionary = new Dictionary<object, SpellStateType>();

        public static void SetAllAnimationSpeed(Card card, float animationSpeed)
        {
            var tweens = iTweenManager.GetTweensForObject(card.gameObject);
            foreach (var iTween in tweens)
            {
                iTween.time = 0.01f;
            }
            var cardActor = card.GetActor();
            if (cardActor != null)
            {
                var attackActorSpell = card.GetActorSpell(SpellType.FRIENDLY_ATTACK);
                if (attackActorSpell != null)
                {
                    try
                    {
                        var spellStateTypeArray = Enum.GetValues(typeof(SpellStateType)) as SpellStateType[];
                        foreach (var type in spellStateTypeArray)
                        {
                            var spellState = attackActorSpell.GetFirstSpellState(type);
                            if (spellState != null)
                            {
                                foreach (var mExternalAnimatedObject in spellState.m_ExternalAnimatedObjects)
                                {
                                    SpellStateType someType;
                                    if (_dictionary.TryGetValue(mExternalAnimatedObject, out someType))
                                    {
                                        if (type != someType)
                                        {
                                            _dictionary[mExternalAnimatedObject] = someType;
                                            Logger.AppendLine($"mExternalAnimatedObject: {mExternalAnimatedObject.m_AnimClip.name} spellType:{type}");

                                        }
                                    }
                                    else
                                    {
                                        _dictionary[mExternalAnimatedObject] = type;
                                        Logger.AppendLine($"mExternalAnimatedObject: {mExternalAnimatedObject.m_AnimClip.name} spellType:{type} m_AnimSpeed:{mExternalAnimatedObject.m_AnimSpeed}");
                                        mExternalAnimatedObject.m_AnimSpeed = 10.0f;
                                    }

                                }
                            }
                        }

                        var activeList = attackActorSpell.GetActiveStateList();
                        if (activeList != null)
                        {
                            foreach (var spellState in activeList)
                            {
                                if (spellState.m_ExternalAnimatedObjects != null)
                                {
                                    foreach (var mExternalAnimatedObject in spellState.m_ExternalAnimatedObjects)
                                    {
                                        mExternalAnimatedObject.m_AnimSpeed = 10.0f;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.AppendLine($"attackActorSpell activeStateList threw {e}");
                    }
                }

            }
        }
    }
}