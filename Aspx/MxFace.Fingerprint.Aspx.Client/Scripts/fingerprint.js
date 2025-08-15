async function initializeDevice() {
    const listResp = await fetch('https://localhost:8034/mfscan/connecteddevicelist', { method: 'POST' });
    const text = await listResp.text();
    const match = /"Connected Device :(.*?)",/.exec(text);
    if (match) {
        const device = match[1];
        await fetch('https://localhost:8034/mfscan/info', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ConnectedDvc: device })
        });
        console.log('Device initialized: ' + device);
    }
}

async function capture() {
    const resp = await fetch('https://localhost:8034/mfscan/capture', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Quality: 60, TimeOut: 10 })
    });
    const data = await resp.json();
    if (data && data.BitmapData) {
        document.getElementById('imgFingerprint').src = 'data:image/bmp;base64,' + data.BitmapData;
        document.getElementById('hfTemplate').value = data.BitmapData;
    }
}
