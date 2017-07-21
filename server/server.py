import json
from flask import Flask, request
app = Flask(__name__)


players_input = {}
game_state = {}


@app.route('/ping')
def ping():
    return 'OK'


@app.route('/get_players_input')
def get_players_input():
    return json.dumps(players_input)


@app.route('/update_player_input', methods=['POST'])
def update_players_input():
    device_id = request.args.get('device_id')
    players_input[device_id] = request.json


@app.route('/get_game_state')
def get_game_state():
    return json.dumps(game_state)


@app.route('/update_game_state', methods=['POST'])
def update_game_state():
    global game_state
    game_state = request.json


@app.route('/inspect')
def inspect():
    return json.dumps(game_state)
