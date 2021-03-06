﻿using Microsoft.Xna.Framework;
using UltimaXNA.Core.Diagnostics;
using UltimaXNA.UltimaGUI;

namespace UltimaXNA.UltimaLogin.Scenes
{
    public class SceneManager
    {
        AScene m_CurrentScene;
        bool m_isTransitioning = false;

        protected UltimaEngine Engine { get; private set; }

        public bool IsTransitioning
        {
            get { return m_isTransitioning; }
        }

        public AScene CurrentScene
        {
            get { return m_CurrentScene; }
            set
            {
                if (m_isTransitioning)
                    return;

                m_isTransitioning = true;

                if (m_CurrentScene != null)
                {
                    Logger.Debug("Starting scene transition from {0} to {1}", m_CurrentScene.GetType().Name, value == null ? "Null" : value.GetType().Name);
                    m_CurrentScene.SceneState = SceneState.TransitioningOff;

                    if (value == null)
                    {
                        m_CurrentScene.Dispose();
                        m_CurrentScene = null;
                    }
                    else
                    {
                        m_CurrentScene.TransitionCompleted += new TransitionCompleteHandler(delegate()
                        {
                            Logger.Debug("Scene transition complete.");
                            Logger.Debug("Disposing {0}.", m_CurrentScene.GetType().Name);

                            m_CurrentScene.Dispose();
                            m_CurrentScene = value;
                            if (m_CurrentScene != null)
                            {
                                m_CurrentScene.Manager = this;

                                if (!m_CurrentScene.IsInitialized)
                                {
                                    Logger.Debug("Initializing {0}.", m_CurrentScene.GetType().Name);
                                    m_CurrentScene.Intitialize(Engine);
                                }
                            }

                            m_isTransitioning = false;
                        });
                    }
                }
                else
                {
                    Logger.Debug("Starting scene {0}", value.GetType().Name);
                    m_CurrentScene = value;
                    m_CurrentScene.Manager = this;

                    if (!m_CurrentScene.IsInitialized)
                    {
                        Logger.Debug("Initializing {0}.", m_CurrentScene.GetType().Name);
                        m_CurrentScene.Intitialize(Engine);
                    }

                    m_isTransitioning = false;
                }
            }
        }

        public SceneManager(UltimaEngine engine)
        {
            Engine = engine;
        }

        public void Update(double totalTime, double frameTime)
        {
            AScene current = m_CurrentScene;

            if (m_CurrentScene != null)
                m_CurrentScene.Update(totalTime, frameTime);

            //This is just incase a scene changes in the middle of updating.
            if (current != m_CurrentScene && m_CurrentScene != null)
            {
                m_CurrentScene.Update(totalTime, frameTime);
            }
        }

        public void ResetToLoginScreen()
        {
            Engine.Client.Disconnect();
            Engine.UserInterface.Reset();
            if (!(m_CurrentScene is LoginScene))
                CurrentScene = new LoginScene();
        }
    }
}
