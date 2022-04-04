// Copyright (c) 2022 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using MTConnect.Agents.Configuration;
using MTConnect.Agents.Metrics;
using MTConnect.Assets;
using MTConnect.Devices;
using MTConnect.Errors;
//using MTConnect.Models;
using MTConnect.Observations;
using MTConnect.Observations.Input;
using MTConnect.Streams;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTConnect.Agents
{
    /// <summary>
    /// An Agent is the centerpiece of an MTConnect implementation. 
    /// It provides two primary functions:
    /// Organizes and manages individual pieces of information published by one or more pieces of equipment.
    /// Publishes that information in the form of a Response Document to client software applications.
    /// </summary>
    public interface IMTConnectAgent
    {
        /// <summary>
        /// Gets the Configuration associated with the Agent
        /// </summary>
        MTConnectAgentConfiguration Configuration { get; }

        /// <summary>
        /// Gets the Metrics associated with the Agent
        /// </summary>
        MTConnectAgentMetrics Metrics { get; }

        /// <summary>
        /// Gets a representation of the specific instance of the Agent.
        /// </summary>
        long InstanceId { get; }

        /// <summary>
        /// Gets the MTConnect Version that the Agent is using.
        /// </summary>
        Version Version { get; set; }

        /// <summary>
        /// Get the configured size of the Buffer in the number of maximum number of DataItems the buffer can hold at one time.
        /// </summary>
        long BufferSize { get; }

        /// <summary>
        /// Get the configured size of the Asset Buffer in the number of maximum number of Assets the buffer can hold at one time.
        /// </summary>
        long AssetBufferSize { get; }

        /// <summary>
        /// A number representing the sequence number assigned to the oldest piece of Streaming Data stored in the buffer
        /// </summary>
        long FirstSequence { get; }

        /// <summary>
        /// A number representing the sequence number assigned to the last piece of Streaming Data that was added to the buffer
        /// </summary>
        long LastSequence { get; }

        /// <summary>
        /// A number representing the sequence number of the piece of Streaming Data that is the next piece of data to be retrieved from the buffer
        /// </summary>
        long NextSequence { get; }


        #region "Event Handlers"

        /// <summary>
        /// Event raised when a new Device is added to the Agent
        /// </summary>
        EventHandler<IDevice> DeviceAdded { get; set; }

        /// <summary>
        /// Raised when a new Observation is attempted to be added to the Agent
        /// </summary>
        EventHandler<IObservationInput> ObservationReceived { get; set; }

        /// <summary>
        /// Raised when a new Observation is successfully added to the Agent
        /// </summary>
        EventHandler<IObservation> ObservationAdded { get; set; }

        /// <summary>
        /// Raised when a new Asset is attempted to be added to the Agent
        /// </summary>
        EventHandler<IAsset> AssetReceived { get; set; }

        /// <summary>
        /// Raised when a new Asset is added to the Agent
        /// </summary>
        EventHandler<IAsset> AssetAdded { get; set; }


        /// <summary>
        /// Raised when an MTConnectDevices response Document is requested from the Agent
        /// </summary>
        MTConnectDevicesRequestedHandler DevicesRequestReceived { get; set; }

        /// <summary>
        /// Raised when an MTConnectDevices response Document is sent successfully from the Agent
        /// </summary>
        MTConnectDevicesHandler DevicesResponseSent { get; set; }

        /// <summary>
        /// Raised when an MTConnectStreams response Document is requested from the Agent
        /// </summary>
        MTConnectStreamsRequestedHandler StreamsRequestReceived { get; set; }

        /// <summary>
        /// Raised when an MTConnectStreams response Document is sent successfully from the Agent
        /// </summary>
        MTConnectStreamsHandler StreamsResponseSent { get; set; }

        /// <summary>
        /// Raised when an MTConnectAssets response Document is requested from the Agent
        /// </summary>
        MTConnectAssetsRequestedHandler AssetsRequestReceived { get; set; }

        /// <summary>
        /// Raised when an MTConnectAssets response Document is sent successfully from the Agent
        /// </summary>
        MTConnectAssetsHandler AssetsResponseSent { get; set; }

        /// <summary>
        /// Raised when an MTConnectError response Document is sent successfully from the Agent
        /// </summary>
        MTConnectErrorHandler ErrorResponseSent { get; set; }


        MTConnectDataItemValidationHandler InvalidDataItemAdded { get; set; }

        #endregion

        #region "Devices"

        /// <summary>
        /// Get a MTConnectDevices Response Document containing all devices.
        /// </summary>
        /// <returns>MTConnectDevices Response Document</returns>
        IDevicesResponseDocument GetDevices(Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectDevices Response Document containing all devices.
        /// </summary>
        /// <returns>MTConnectDevices Response Document</returns>
        Task<IDevicesResponseDocument> GetDevicesAsync(Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectDevices Response Document containing the specified device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <returns>MTConnectDevices Response Document</returns>
        IDevicesResponseDocument GetDevices(string deviceKey, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectDevices Response Document containing the specified device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <returns>MTConnectDevices Response Document</returns>
        Task<IDevicesResponseDocument> GetDevicesAsync(string deviceKey, Version mtconnectVersion = null);

        #endregion

        #region "Streams"

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="count">The Maximum Number of DataItems to return</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStreams(int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="count">The Maximum Number of DataItems to return</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamsAsync(int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="at">The sequence number to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStreams(long at, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="at">The sequence number to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamsAsync(long at, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="at">The sequence number to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStreams(IEnumerable<string> dataItemIds, long at, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="at">The sequence number to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamsAsync(IEnumerable<string> dataItemIds, long at, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="from">The sequence number of the first observation to include in the response</param>
        /// <param name="to">The sequence number of the last observation to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStreams(long from, long to, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="from">The sequence number of the first observation to include in the response</param>
        /// <param name="to">The sequence number of the last observation to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamsAsync(long from, long to, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="from">The sequence number of the first observation to include in the response</param>
        /// <param name="to">The sequence number of the last observation to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStreams(IEnumerable<string> dataItemIds, long from, long to, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing all devices.
        /// </summary>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="from">The sequence number of the first observation to include in the response</param>
        /// <param name="to">The sequence number of the last observation to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamsAsync(IEnumerable<string> dataItemIds, long from, long to, int count = 0, Version mtconnectVersion = null);


        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStream(string deviceKey, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamAsync(string deviceKey, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStream(string deviceKey, IEnumerable<string> dataItemIds, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamAsync(string deviceKey, IEnumerable<string> dataItemIds, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="at">The sequence number to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStream(string deviceKey, long at, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="at">The sequence number to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamAsync(string deviceKey, long at, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="at">The sequence number to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStream(string deviceKey, IEnumerable<string> dataItemIds, long at, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="at">The sequence number to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamAsync(string deviceKey, IEnumerable<string> dataItemIds, long at, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="from">The sequence number of the first observation to include in the response</param>
        /// <param name="to">The sequence number of the last observation to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStream(string deviceKey, long from, long to, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="from">The sequence number of the first observation to include in the response</param>
        /// <param name="to">The sequence number of the last observation to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamAsync(string deviceKey, long from, long to, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="from">The sequence number of the first observation to include in the response</param>
        /// <param name="to">The sequence number of the last observation to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        IStreamsResponseDocument GetDeviceStream(string deviceKey, IEnumerable<string> dataItemIds, long from, long to, int count = 0, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectStreams Document containing the specified Device.
        /// </summary>
        /// <param name="deviceKey">The (name or uuid) of the requested Device</param>
        /// <param name="dataItemIds">A list of DataItemId's to specify what observations to include in the response</param>
        /// <param name="from">The sequence number of the first observation to include in the response</param>
        /// <param name="to">The sequence number of the last observation to include in the response</param>
        /// <param name="count">The maximum number of observations to include in the response</param>
        /// <returns>MTConnectStreams Response Document</returns>
        Task<IStreamsResponseDocument> GetDeviceStreamAsync(string deviceKey, IEnumerable<string> dataItemIds, long from, long to, int count = 0, Version mtconnectVersion = null);

        #endregion

        #region "Assets"

        /// <summary>
        /// Get an MTConnectAssets Document containing all Assets.
        /// </summary>
        /// <param name="type">Defines the type of MTConnect Asset to be returned in the MTConnectAssets Response Document.</param>
        /// <param name="removed">
        /// An attribute that indicates whether the Asset has been removed from a piece of equipment.
        /// If the value of the removed parameter in the query is true, then Asset Documents for Assets that have been marked as removed from a piece of equipment will be included in the Response Document.
        /// If the value of the removed parameter in the query is false, then Asset Documents for Assets that have been marked as removed from a piece of equipment will not be included in the Response Document.
        /// </param>
        /// <param name="count">Defines the maximum number of Asset Documents to return in an MTConnectAssets Response Document.</param>
        /// <returns>MTConnectAssets Response Document</returns>
        IAssetsResponseDocument GetAssets(string type = null, bool removed = false, int count = 100, Version mtconnectVersion = null);

        /// <summary>
        /// Get an MTConnectAssets Document containing all Assets.
        /// </summary>
        /// <param name="type">Defines the type of MTConnect Asset to be returned in the MTConnectAssets Response Document.</param>
        /// <param name="removed">
        /// An attribute that indicates whether the Asset has been removed from a piece of equipment.
        /// If the value of the removed parameter in the query is true, then Asset Documents for Assets that have been marked as removed from a piece of equipment will be included in the Response Document.
        /// If the value of the removed parameter in the query is false, then Asset Documents for Assets that have been marked as removed from a piece of equipment will not be included in the Response Document.
        /// </param>
        /// <param name="count">Defines the maximum number of Asset Documents to return in an MTConnectAssets Response Document.</param>
        /// <returns>MTConnectAssets Response Document</returns>
        Task<IAssetsResponseDocument> GetAssetsAsync(string type = null, bool removed = false, int count = 100, Version mtconnectVersion = null);


        /// <summary>
        /// Get a MTConnectAssets Document containing the specified Asset
        /// </summary>
        /// <param name="assetId">The ID of the Asset to include in the response</param>
        /// <returns>MTConnectAssets Response Document</returns>
        IAssetsResponseDocument GetAsset(string assetId, Version mtconnectVersion = null);

        /// <summary>
        /// Get a MTConnectAssets Document containing the specified Asset
        /// </summary>
        /// <param name="assetId">The ID of the Asset to include in the response</param>
        /// <returns>MTConnectAssets Response Document</returns>
        Task<IAssetsResponseDocument> GetAssetAsync(string assetId, Version mtconnectVersion = null);

        #endregion

        #region "Errors"

        /// <summary>
        /// Get an MTConnectErrors Document containing the specified ErrorCode
        /// </summary>
        /// <param name="errorCode">Provides a descriptive code that indicates the type of error that was encountered by an Agent when attempting to respond to a Request for information.</param>
        /// <param name="cdata">The CDATA for Error contains a textual description of the error and any additional information an Agent is capable of providing regarding a specific error.</param>
        /// <returns>MTConnectError Response Document</returns>
        IErrorResponseDocument GetError(ErrorCode errorCode, string cdata = null);

        /// <summary>
        /// Get an MTConnectErrors Document containing the specified ErrorCode
        /// </summary>
        /// <param name="errorCode">Provides a descriptive code that indicates the type of error that was encountered by an Agent when attempting to respond to a Request for information.</param>
        /// <param name="cdata">The CDATA for Error contains a textual description of the error and any additional information an Agent is capable of providing regarding a specific error.</param>
        /// <returns>MTConnectError Response Document</returns>
        Task<IErrorResponseDocument> GetErrorAsync(ErrorCode errorCode, string cdata = null);

        /// <summary>
        /// Get an MTConnectErrors Document containing the specified Errors
        /// </summary>
        /// <param name="errors">A list of Errors to include in the response Document</param>
        /// <returns>MTConnectError Response Document</returns>
        IErrorResponseDocument GetError(IEnumerable<IError> errors);

        /// <summary>
        /// Get an MTConnectErrors Document containing the specified Errors
        /// </summary>
        /// <param name="errors">A list of Errors to include in the response Document</param>
        /// <returns>MTConnectError Response Document</returns>
        Task<IErrorResponseDocument> GetErrorAsync(IEnumerable<IError> errors);

        #endregion


        #region "Add"

        /// <summary>
        /// Add a new MTConnectDevice to the Agent's Buffer
        /// </summary>
        bool AddDevice(IDevice device);

        /// <summary>
        /// Add a new MTConnectDevice to the Agent's Buffer
        /// </summary>
        Task<bool> AddDeviceAsync(IDevice device);

        /// <summary>
        /// Add new MTConnectDevices to the Agent's Buffer
        /// </summary>
        bool AddDevices(IEnumerable<IDevice> devices);

        /// <summary>
        /// Add new MTConnectDevices to the Agent's Buffer
        /// </summary>
        Task<bool> AddDevicesAsync(IEnumerable<IDevice> devices);


        /// <summary>
        /// Add a new Observation for a DataItem of category EVENT or SAMPLE to the Agent
        /// </summary>
        bool AddObservation(string deviceKey, string dataItemId, object value);

        /// <summary>
        /// Add a new Observation for a DataItem of category EVENT or SAMPLE to the Agent
        /// </summary>
        Task<bool> AddObservationAsync(string deviceKey, string dataItemId, object value);

        /// <summary>
        /// Add a new Observation for a DataItem of category EVENT or SAMPLE to the Agent
        /// </summary>
        bool AddObservation(string deviceKey, string dataItemId, string valueKey, object value);

        /// <summary>
        /// Add a new Observation for a DataItem of category EVENT or SAMPLE to the Agent
        /// </summary>
        Task<bool> AddObservationAsync(string deviceKey, string dataItemId, string valueKey, object value);

        /// <summary>
        /// Add a new Observation for a DataItem of category EVENT or SAMPLE to the Agent
        /// </summary>
        bool AddObservation(string deviceKey, IObservationInput observation);

        /// <summary>
        /// Add a new Observation for a DataItem of category EVENT or SAMPLE to the Agent
        /// </summary>
        Task<bool> AddObservationAsync(string deviceKey, IObservationInput observation);

        /// <summary>
        /// Add new Observations for DataItems of category EVENT or SAMPLE to the Agent
        /// </summary>
        bool AddObservations(string deviceKey, IEnumerable<IObservationInput> observations);

        /// <summary>
        /// Add new Observations for DataItems of category EVENT or SAMPLE to the Agent
        /// </summary>
        Task<bool> AddObservationsAsync(string deviceKey, IEnumerable<IObservationInput> observations);


        /// <summary>
        /// Add a new Asset to the Agent
        /// </summary>
        bool AddAsset(string deviceKey, IAsset asset);

        /// <summary>
        /// Add a new Asset to the Agent
        /// </summary>
        Task<bool> AddAssetAsync(string deviceKey, IAsset asset);

        /// <summary>
        /// Add new Assets to the Agent
        /// </summary>
        bool AddAssets(string deviceKey, IEnumerable<IAsset> assets);

        /// <summary>
        /// Add new Assets to the Agent
        /// </summary>
        Task<bool> AddAssetsAsync(string deviceKey, IEnumerable<IAsset> assets);


        ///// <summary>
        ///// Add a new DeviceModel to the Agent's Buffer. This adds all of the data contained in the Device Model (Device and Observations).
        ///// </summary>
        //bool AddDeviceModel(DeviceModel deviceModel);

        ///// <summary>
        ///// Add a new DeviceModel to the Agent's Buffer. This adds all of the data contained in the Device Model (Device and Observations).
        ///// </summary>
        //Task<bool> AddDeviceModelAsync(DeviceModel deviceModel);

        ///// <summary>
        ///// Add new DeviceModels to the Agent's Buffer. This adds all of the data contained in the Device Models (Device and Observations).
        ///// </summary>
        //bool AddDeviceModels(IEnumerable<DeviceModel> deviceModels);

        ///// <summary>
        ///// Add new DeviceModels to the Agent's Buffer. This adds all of the data contained in the Device Models (Device and Observations).
        ///// </summary>
        //Task<bool> AddDeviceModelsAsync(IEnumerable<DeviceModel> deviceModels);

        #endregion

        #region "Interfaces"

        // Task<IEnumerable<Interfaces.Interface>> GetInterfaces();

        // Task<IEnumerable<Interfaces.Interface>> GetInterfaces(string deviceName);

        // Task<IEnumerable<Interfaces.Interface>> GetInterfaces(IEnumerable<string> deviceNames);

        // Task<Interfaces.Interface> GetInterfaces(string deviceName, string interfaceId);

        // Task<IEnumerable<Interfaces.Interface>> GetInterfaces(string deviceName, IEnumerable<string> interfaceIds);


        // Task<Interfaces.InterfaceState> GetInterfaceState(string deviceName, string interfaceId);

        // Task<Interfaces.InterfaceRequestState> GetRequestState(string deviceName, string interfaceId);

        // Task<Interfaces.InterfaceResponseState> GetResponseState(string deviceName, string interfaceId);

        #endregion
    }
}