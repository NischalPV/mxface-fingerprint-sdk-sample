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

### Prerequisites
- Windows machine with **IIS 10+** installed
- [.NET Framework 4.8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet-framework) and **ASP.NET 4.8** features enabled
- "Static Content" feature enabled for serving CSS/JS
- Valid TLS certificate if the API requires HTTPS
- MxFace Fingerprint API accessible from the server
- MFScan device service installed on client machines for local device access

### Setup
1. Copy the project folder to the server, e.g. `C:\inetpub\MxFace.Fingerprint.Aspx.Client`.
2. In **IIS Manager**, create a new **Application Pool** targeting **.NET CLR v4.0** with the pipeline mode set to *Integrated*.
3. Grant the Application Pool identity read permissions on the project folder.
4. Add a new **Website** or **Application** pointing to the folder and assign the Application Pool.
5. Configure site bindings and install a TLS certificate (HTTPS is recommended).
6. Update `Web.config` if the fingerprint API base URL differs from the default (`https://localhost:7103/`).
7. Start the site and browse to `Default.aspx`. The page will load even if the API or device service is offline, displaying helpful error messages.

## License
This sample is provided for demonstration purposes.
