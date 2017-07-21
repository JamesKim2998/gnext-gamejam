import json
from flask import Flask, request
app = Flask(__name__)


players = {}


@app.route('/ping')
def ping():
    return 'OK'


@app.route('/update_player', methods=['POST'])
def update_player():
    device_id = request.args.get('device_id')
    players[device_id] = request.json
    players_except_me = {}
    for test_device_id, test_player in players.items():
        if device_id == test_device_id:
            continue
        players_except_me[test_device_id] = test_player
    return json.dumps(players_except_me)


@app.route('/inspect')
def inspect():
    return json.dumps(players)
