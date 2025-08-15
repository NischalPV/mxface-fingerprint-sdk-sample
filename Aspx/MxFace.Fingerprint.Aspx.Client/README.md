# MxFace.Fingerprint.Aspx.Client

This sample demonstrates a standalone ASP.NET Web Forms application targeting **.NET Framework 4.8** for working with the MxFace fingerprint SDK. It mirrors the features of the existing API client but runs entirely in a classic Web Forms environment.

## Features
- Initialize the fingerprint device from the browser using JavaScript.
- Capture fingerprint images on the client and display them immediately.
- Enroll, search and verify fingerprints against the MxFace Fingerprint API.
- Simple WebForms UI implemented in `Default.aspx`.

## Implementation Notes
- Device discovery and capture are performed with `fetch` requests to the local device service (`https://localhost:8034/mfscan`).
- Captured bitmap data is stored in a hidden field so server-side postbacks can send it to the API.
- `App_Code/Services` contains `FingerprintApiService` for calling the REST API (`https://localhost:7103/`).
- Models required by the services are provided under `App_Code/Models`.

## Running the Sample
1. Open `MxFace.Fingerprint.Aspx.Client.sln` in Visual Studio 2022.
2. Restore NuGet packages and build the solution.
3. Press **F5** to run with IIS Express, or deploy to IIS as described below.

## IIS Hosting
1. Copy the project folder to the server.
2. In **IIS Manager**, create a new Application Pool targeting **.NET CLR v4.0** and set the pipeline mode to *Integrated*.
3. Add a new **Website** or **Application** pointing to the project folder and assign the newly created Application Pool.
4. Ensure the site is served over HTTPS if the API requires it.
5. Browse to the site and use the UI to initialize the device and capture fingerprints.

## License
This sample is provided for demonstration purposes.
