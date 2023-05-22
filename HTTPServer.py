from http.server import BaseHTTPRequestHandler, HTTPServer
import json
import time
import random
import hashlib
import requests

PORT_NUMBER = 8080

def create_sign(access_token, app_id, key_id, nonce, time, app_key):
    sb = []
    if access_token:
        sb.append("Accesstoken=" + access_token + "&")
    sb.append("Appid=" + app_id + "&")
    sb.append("Keyid=" + key_id + "&")
    sb.append("Nonce=" + nonce + "&")
    sb.append("Time=" + time + app_key)
    sign_str = "".join(sb).lower()
    return md5_32(sign_str)

def md5_32(source_str):
    md5 = hashlib.md5()
    md5.update(source_str.encode('utf-8'))
    return md5.hexdigest()

def get_illuminance_data():
    url = "https://open-cn.aqara.com/v3.0/open/api"

    access_token = "168b28c91985204f583838efd74a86af"
    app_id = "1096443083894595584212fb"
    key_id = "K.1096443084720873472"
    app_key = "0qplgbjckx0rf3m4mxgip8us19vjyf9w"

    nonce = "".join([chr(random.randint(97, 122)) for _ in range(16)])
    timestamp = str(int(time.time() * 1000))

    data = {
        "intent": "query.resource.value",
        "data": {
            "resources": [
                {
                    "subjectId": "lumi1.54ef444b51d9",
                    "resourceIds": [
                        "0.4.85"
                    ]
                }
            ]
        }
    }

    sign = create_sign(access_token, app_id, key_id, nonce, timestamp, app_key)

    headers = {
        "Accesstoken": access_token,
        "Appid": app_id,
        "Keyid": key_id,
        "Nonce": nonce,
        "Time": timestamp,
        "Sign": sign,
        "Content-Type": "application/json"
    }

    response = requests.post(url, headers=headers, data=json.dumps(data))
    return response.json()

class RequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.send_header('Content-type','application/json')
        self.end_headers()

        illuminance_data = get_illuminance_data()
        self.wfile.write(json.dumps(illuminance_data).encode())

try:
    server = HTTPServer(('', PORT_NUMBER), RequestHandler)
    print('Started http server on port', PORT_NUMBER)
    server.serve_forever()
except KeyboardInterrupt:
    server.socket.close()
