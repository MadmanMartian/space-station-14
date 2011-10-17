﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SS3D_shared;
using SS3D_shared.GO;
using GorgonLibrary.Graphics;

namespace CGO
{
    public class WearableSpriteComponent : SpriteComponent
    {
        string basename = "";
        private bool worn = false;
        public WearableSpriteComponent()
            : base()
        {
            DrawDepth = 2; //Floor drawdepth
        }

        public override void RecieveMessage(object sender, ComponentMessageType type, List<ComponentReplyMessage> replies, params object[] list)
        {
            base.RecieveMessage(sender, type, replies, list);

            switch (type)
            {
                case ComponentMessageType.MoveDirection:
                    switch ((Constants.MoveDirs)list[0])
                    {
                        case Constants.MoveDirs.north:
                            if (worn)
                            {
                                SetSpriteByKey(basename + "_back");
                                flip = true;
                            }
                            else
                                SetSpriteByKey(basename);
                            break;
                        case Constants.MoveDirs.south:
                            if (worn)
                            {
                                SetSpriteByKey(basename + "_front");
                            }
                            else
                                SetSpriteByKey(basename);
                            
                            break;
                        case Constants.MoveDirs.east:
                            if (worn)
                            {
                                SetSpriteByKey(basename + "_side");
                                flip = true;
                            }
                            else
                                SetSpriteByKey(basename);
                            break;
                        case Constants.MoveDirs.west:
                            if (worn)
                            {
                                SetSpriteByKey(basename + "_side");
                                flip = false;
                            }
                            else
                                SetSpriteByKey(basename);
                            break;
                        case Constants.MoveDirs.northeast:
                            if (worn)
                            {
                                SetSpriteByKey(basename + "_back");
                            }
                            else
                                SetSpriteByKey(basename);
                            break;
                        case Constants.MoveDirs.northwest:
                            if (worn)
                            {
                                SetSpriteByKey(basename + "_back");
                            }
                            else
                                SetSpriteByKey(basename);
                            break;
                        case Constants.MoveDirs.southeast:
                            if (worn)
                            {
                                SetSpriteByKey(basename + "_front");
                            }
                            else
                                SetSpriteByKey(basename);
                            break;
                        case Constants.MoveDirs.southwest:
                            if (worn)
                            {
                                SetSpriteByKey(basename + "_front");
                            }
                            else
                                SetSpriteByKey(basename);
                            break;
                    }
                    DrawDepth = 4;
                    break;
                case ComponentMessageType.ItemDetach:
                    SetSpriteByKey(basename);
                    DrawDepth = 2;
                    break;
                case ComponentMessageType.ItemWorn:
                    worn = true;
                    DrawDepth = 4;
                    break;
                case ComponentMessageType.ItemUnWorn:
                    worn = false;
                    break;
            }

            return;
        }

        /// <summary>
        /// Set parameters :)
        /// </summary>
        /// <param name="parameter"></param>
        public override void SetParameter(ComponentParameter parameter)
        {
            //base.SetParameter(parameter);
            switch (parameter.MemberName)
            {
                case "basename":
                    basename = (string)parameter.Parameter;
                    LoadSprites();
                    break;
            }
        }

        protected override Sprite GetBaseSprite()
        {
            return sprites[basename];
        }

        /// <summary>
        /// Load the mob sprites given the base name of the sprites.
        /// </summary>
        public void LoadSprites()
        {
            AddSprite(basename);
            AddSprite(basename + "_front");
            AddSprite(basename + "_back");
            AddSprite(basename + "_side");

            SetSpriteByKey(basename);
        }
    }
}