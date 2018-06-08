import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface LoginState {
    username: string;
    password: string;
}

export class Login extends React.Component<RouteComponentProps<{}>, LoginState> {
    constructor() {
        super();
        this.state = { username: '', password: '' };

        this.handleUsernameChange = this.handleUsernameChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleUsernameChange(event: { target: { value: any; }; }) {
        this.setState({ username: event.target.value });
    }

    handlePasswordChange(event: { target: { value: any; }; }) {
        this.setState({ password: event.target.value });
    }

    handleSubmit(event: { preventDefault: () => void; }) {
        event.preventDefault();
        alert('Username: ' + this.state.username);
        alert('Password: ' + this.state.password);
        let requestData = new FormData();
        requestData.append("Username", this.state.username);
        requestData.append("Password", this.state.password);
        fetch('/GetToken', {
            method: 'POST',
            body: requestData,
        }).then((response) => response.json())
            .then((responseJson) => {
                var data = responseJson;
                alert(data);
            })
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Email:
                    <input type="text" value={this.state.username} onChange={this.handleUsernameChange} />
                </label>
                <br />
                <label>
                    Password:
                    <input type="password" value={this.state.password} onChange={this.handlePasswordChange} />
                </label>
                <br />
                <input type="submit" value="Submit" />
            </form>
        );
    }
}

