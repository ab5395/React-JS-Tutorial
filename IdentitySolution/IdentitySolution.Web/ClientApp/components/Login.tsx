import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';

interface LoginState {
    loginData: LoginForm;
}

export class LoginForm {
    username: string = "";
    password: string = "";
}



export class Login extends React.Component<RouteComponentProps<{}>, LoginState> {
    constructor() {
        super();
        this.state = { loginData: new LoginForm };

        this.handleLogin = this.handleLogin.bind(this);
    }

    public render() {
        return <div>
            {this.renderLoginForm()}
        </div>;
    }

    public renderLoginForm() {
        return <div>
            <h2>Login Form</h2>
            <form onSubmit={this.handleLogin}>
                <div>
                    <label>Email</label>
                    <input type="email" name="email" value={this.state.loginData.username} required />
                </div>
                <div>
                    <label>Password</label>
                    <input type="password" name="password" value={this.state.loginData.password} required />
                </div>
                <button type="submit" className="btn btn-default">Login</button>
            </form>
        </div>;
    }

    private handleLogin(event) {
        event.preventDefault();
        const data = new FormData(event.target);

        fetch('api/Employee/Create', {
            method: 'POST',
            body: data,

        }).then((response) => response.json())
            .then((responseJson) => {
                this.props.history.push("/fetchemployee");
            })
    }
}