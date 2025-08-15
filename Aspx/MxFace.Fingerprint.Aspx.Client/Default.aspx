<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<!DOCTYPE html>
<html>
<head>
    <title>MxFace Fingerprint ASPX Client</title>
    <link rel="stylesheet" href="bootstrap/bootstrap.min.css" />
    <link rel="stylesheet" href="app.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="Scripts/MFScan.js"></script>
    <script type="text/javascript">
        var firstDevice = '';
        $(document).ready(function () {
            try {
                var list = GetConnectedDeviceList();
                if (list && list.httpStatus && list.data && list.data.data && list.data.data.length > 0) {
                    var dev = list.data.data[0];
                    var match = /Connected Device :(.*)/.exec(dev);
                    if (match) {
                        firstDevice = match[1].trim();
                        var info = GetMorFinAuthInfo(firstDevice, '');
                        if (info && info.httpStatus && info.data) {
                            var d = info.data;
                            $('#spnSerialNo').text(d.DeviceInfo.SerialNo);
                            $('#spnModel').text(d.DeviceInfo.Model);
                            $('#spnMake').text(d.DeviceInfo.Make);
                            $('#spnStatus').text(d.ErrorDescription);
                        } else if (info && !info.httpStatus) {
                            $('#spnStatus').text(info.err);
                        }
                    }
                } else if (list && !list.httpStatus) {
                    $('#spnStatus').text(list.err);
                } else {
                    $('#spnStatus').text('Device service unavailable');
                }
            } catch (e) {
                $('#spnStatus').text(e.message);
            }
        });

        function capture(target) {
            try {
                var res = CaptureFingerprint(60, 10);
                if (res && res.httpStatus && res.data) {
                    var data = res.data;
                    var img = 'data:image/bmp;base64,' + data.BitmapData;
                    if (target === 'capture1') {
                        $('#imgCapture1').attr('src', img);
                        $('#imgMatch1').attr('src', img);
                        $('#hfTemplate1').val(data.BitmapData);
                        $('#txtStatus').val(data.ErrorDescription);
                        $('#txtQuality').val(data.Quality);
                        $('#txtImageData').val(data.BitmapData);
                    } else if (target === 'capture2') {
                        $('#imgCapture2').attr('src', img);
                        $('#hfTemplate2').val(data.BitmapData);
                    }
                } else if (res && !res.httpStatus) {
                    $('#txtStatus').val(res.err);
                }
            } catch (e) {
                $('#txtStatus').val(e.message);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="container mt-3">
        <div class="custom_card">
            <p class="heading_title">Capture</p>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="d-flex justify-content-between">
                        <div class="custom_card details_box w-100 mb-0">
                            <div class="detail_item_box"><strong>Serial No:</strong> <span id="spnSerialNo"></span></div>
                            <div class="detail_item_box"><strong>Model:</strong> <span id="spnModel"></span></div>
                            <div class="detail_item_box"><strong>Make:</strong> <span id="spnMake"></span></div>
                            <div class="detail_item_box"><strong>Status:</strong> <span id="spnStatus" class="text-warning"></span></div>
                        </div>
                        <div class="custom_card details_box w-100 ms-3 mb-0 text-center">
                            <div class="captured_img">
                                <img id="imgCapture1" class="img-thumbnail" src="data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==" alt="Capture 1" />
                            </div>
                            <button type="button" id="btnCapture1" class="btn btn_primary mt-3 w-100" onclick="capture('capture1')">
                                <i class="fas fa-camera"></i> Capture 1
                            </button>
                            <asp:HiddenField ID="hfTemplate1" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-6">
                    <div class="custom_card mb-0 h-100">
                        <div class="row">
                            <div class="col-6">
                                <label for="txtStatus" class="form-label fw-bold text-secondary">Status:</label>
                                <input type="text" id="txtStatus" class="form-control" disabled />
                            </div>
                            <div class="col-6">
                                <label for="txtQuality" class="form-label fw-bold text-secondary">Quality:</label>
                                <input type="text" id="txtQuality" class="form-control" disabled />
                            </div>
                            <div class="col-12 mt-3">
                                <label for="txtImageData" class="form-label fw-bold text-secondary">Image Data:</label>
                                <textarea id="txtImageData" class="form-control" rows="3" disabled></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="custom_card">
            <div class="row">
                <div class="col-md-6 col-12">
                    <p class="heading_title">Enroll</p>
                    <div class="custom_card m-h-220px mb-0">
                        <div class="row g-3 align-items-end">
                            <div class="col-6">
                                <label for="txtGroup" class="form-label fw-bold text-secondary">Group:</label>
                                <asp:TextBox ID="txtGroup" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-6">
                                <label for="txtPersonId" class="form-label fw-bold text-secondary">Code:</label>
                                <asp:TextBox ID="txtPersonId" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="mt-3 w-100">
                            <asp:Button ID="btnEnroll" CssClass="btn btn_primary w-100" runat="server" Text="Enroll" OnClick="Enroll_Click" />
                        </div>
                        <div class="d-flex align-items-center mt-3">
                            <div class="w-50">
                                <label for="lblEnrollCode" class="form-label fw-bold text-secondary">Response Code:</label>
                                <asp:Label ID="lblEnrollCode" CssClass="detail_item_box p-3 text-success" runat="server" />
                            </div>
                            <div class="w-50 ms-3">
                                <label for="lblEnrollMsg" class="form-label fw-bold text-secondary">Message:</label>
                                <asp:Label ID="lblEnrollMsg" CssClass="detail_item_box p-3 text-success" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-12">
                    <p class="heading_title">Search</p>
                    <div class="custom_card m-h-220px mb-0">
                        <div class="col-md-12 mt-2">
                            <asp:Button ID="btnSearch" CssClass="btn btn_primary w-100" runat="server" Text="Search" OnClick="Search_Click" />
                        </div>
                        <div class="row g-3 align-items-center mt-4">
                            <div class="col-md-6">
                                <label for="txtSearchScore" class="form-label fw-bold text-secondary">Matching Score:</label>
                                <asp:TextBox ID="txtSearchScore" CssClass="form-control" runat="server" Enabled="false" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtSearchImage" class="form-label fw-bold text-secondary">Image Data:</label>
                                <asp:TextBox ID="txtSearchImage" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine" Rows="2" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="custom_card">
            <p class="heading_title">Match</p>
            <div class="row g-3 align-items-center">
                <div class="col-md-6">
                    <div class="d-flex justify-content-between">
                        <div class="custom_card w-100 mb-0 text-center">
                            <p class="text_primary">Capture 1</p>
                            <div class="captured_img">
                                <img id="imgMatch1" alt="Capture 1" class="img-thumbnail" src="data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==" height="220" width="220" />
                            </div>
                        </div>
                        <div class="custom_card w-100 mb-0 text-center ms-3">
                            <p class="text_primary">Capture 2</p>
                            <div class="captured_img">
                                <img id="imgCapture2" alt="Capture 2" class="img-thumbnail" src="data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==" height="220" width="220" />
                            </div>
                            <div class="mt-3">
                                <button type="button" class="btn btn_primary w-100" onclick="capture('capture2')">Capture 2</button>
                                <asp:HiddenField ID="hfTemplate2" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnMatch" CssClass="btn btn_primary w-100" runat="server" Text="Match" OnClick="Verify_Click" />
                </div>
                <div class="col-md-4">
                    <div class="custom_card mb-0">
                        <label for="txtMatchScore" class="form-label fw-bold text-secondary">Match:</label>
                        <asp:TextBox ID="txtMatchScore" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
