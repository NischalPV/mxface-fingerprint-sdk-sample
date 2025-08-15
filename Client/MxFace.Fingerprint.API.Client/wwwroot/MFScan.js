const uri = "http://localhost:8034/mfscan/";

function GetConnectedDeviceList() {
  return PostRequestAsync("connecteddevicelist", "", 0);
}

function GetMorFinAuthInfo(connectedDvc, clientKey) {
  var MorFinAuthRequest = {
    ConnectedDvc: connectedDvc,
    ClientKey: clientKey,
  };
  var jsondata = JSON.stringify(MorFinAuthRequest);
  return PostRequestAsync("info", jsondata);
}

function CaptureFingerprint(quality, timeout) {
  var MorFinAuthRequest = {
    Quality: quality,
    TimeOut: timeout,
  };
  var jsondata = JSON.stringify(MorFinAuthRequest);
  return PostRequestAsync("capture", jsondata);
}

function PostRequestAsync(method, jsonData, isBodyAvailable) {
  var res;
  if (isBodyAvailable == 0) {
    $.support.cors = true;
    var httpStatus = false;
    $.ajax({
      type: "POST",
      async: false,
      crossDomain: true,
      url: uri + method,
      contentType: "application/json; charset=utf-8",
      //data: jsonData,
      dataType: "json",
      processData: false,
      success: function (data) {
        httpStatus = true;
        res = { httpStatus: httpStatus, data: data };
      },
      error: function (jqXHR, ajaxOptions, thrownError) {
        res = { httpStatus: httpStatus, err: getHttpError(jqXHR) };
      },
    });
  } else {
    $.support.cors = true;
    var httpStatus = false;
    $.ajax({
      type: "POST",
      async: false,
      crossDomain: true,
      url: uri + method,
      contentType: "application/json; charset=utf-8",
      data: jsonData,
      dataType: "json",
      processData: false,
      success: function (data) {
        httpStatus = true;
        res = { httpStatus: httpStatus, data: data };
      },
      error: function (jqXHR, ajaxOptions, thrownError) {
        res = { httpStatus: httpStatus, err: getHttpError(jqXHR) };
      },
    });
  }
  return res;
}

function getHttpError(jqXHR) {
  var err = "Unhandled exception";
  if (jqXHR.status === 0) {
    err = "Service Unavailable";
  } else if (jqXHR.status == 404) {
    err = "Requested page not found";
  } else if (jqXHR.status == 500) {
    err = "Internal Server Error";
  } else if (thrownError === "parsererror") {
    err = "Requested JSON parse failed";
  } else if (thrownError === "timeout") {
    err = "Time out error";
  } else if (thrownError === "abort") {
    err = "Ajax request aborted";
  } else {
    err = "Unhandled Error";
  }
  return err;
}
