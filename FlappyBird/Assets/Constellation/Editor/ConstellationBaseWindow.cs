﻿namespace ConstellationEditor {
    public class ConstellationBaseWindow : ExtendedEditorWindow, ILoadable {
        protected ConstellationEditorDataService scriptDataService;
        protected ConstellationCompiler ConstellationCompiler;
        static protected bool canDrawUI = false;
        protected ConstellationInstanceObject[] CurrentEditedInstancesName;

        public void Awake () {
            Setup ();
            canDrawUI = false;
        }

        protected virtual void Setup () { }

        public void New () {
            scriptDataService = new ConstellationEditorDataService ();
            scriptDataService.New ();
            Setup ();
        }

        public void Recover () {
            scriptDataService = new ConstellationEditorDataService ();
            ConstellationCompiler = new ConstellationCompiler ();
            if (scriptDataService.OpenEditorData ().LastOpenedConstellationPath == null)
                return;

            if (scriptDataService.OpenEditorData ().LastOpenedConstellationPath.Count != 0) {
                var scriptData = scriptDataService.Recover (scriptDataService.OpenEditorData ().LastOpenedConstellationPath[0]);
                if (scriptData != null) {
                    Setup ();
                    return;
                }
            }
        }

        public void ResetInstances () {
            scriptDataService.RessetInstancesPath ();
        }

        public void OpenConstellationInstance (Constellation.Constellation constellation, string path) {
            scriptDataService = new ConstellationEditorDataService ();
            scriptDataService.OpenConstellationInstance (constellation, path);
            CurrentEditedInstancesName = scriptDataService.currentInstancePath.ToArray ();
            Setup ();
        }

        public void Open (string _path = "") {
            scriptDataService = new ConstellationEditorDataService ();
            scriptDataService.OpenConstellation (_path);
            Setup ();
        }

        public void Save () {
            scriptDataService.Save ();
        }

        public void SaveInstance () {
            scriptDataService.SaveInstance ();
        }

        protected bool IsConstellationSelected () {
            if (scriptDataService != null) {
                if (scriptDataService.GetCurrentScript () != null)
                    return true;
                else
                    return false;
            } else
                return false;

        }
    }
}