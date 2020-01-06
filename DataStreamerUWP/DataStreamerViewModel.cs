﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED AS IS WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Core;

namespace Microsoft.DataStreamer.UWP
{
    /// <summary>
    /// Base view model for Data Streamer applications
    /// </summary>
    public abstract class DataStreamerViewModel : INotifyPropertyChanged
    {
        private string _apiVersion = "";
        private string _appVersion = "";
        private string _status     = "Not Connected";

        public DataStreamerViewModel()
        {
        }

        public string ApiVersion   => _apiVersion;
        public string AppVersion   => _appVersion;
        public string Status       => _status;
        public bool   IsReady      => _status == "Ready";
        public bool   IsStreaming  => _status == "Reading";
        public bool   IsRecording  => _status == "Recording";

        public void SetApiVersion(string val)
        { 
            _apiVersion = val;
            OnPropertyChanged("ApiVersion"); 
        }            
            
        public void SetAppVersion(string val)
        { 
            _appVersion = val;
            OnPropertyChanged("AppVersion"); 
        } 
                    
        public void SetStatus(string val)
        { 
            _status = val;
            OnPropertyChanged("Status"); 
            OnPropertyChanged("IsReady"); 
            OnPropertyChanged("IsStreaming"); 
            OnPropertyChanged("IsRecording"); 
        } 
        
        public CoreDispatcher Dispatcher { get; set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion 

        protected void OnPropertyChanged(string name)
        { 
            var dispatcher = this.Dispatcher;

            if(dispatcher != null)
            { 
                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 
                }).AsTask().FireAndForget();
            }
        } 
    }
}