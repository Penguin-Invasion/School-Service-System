import { useState } from "react";
import PropTypes from 'prop-types';

import {
  Card,
  CardBody,
  FormGroup,
  InputGroupAddon,
  InputGroupText,
  InputGroup,
  Row,
  Col,
} from "reactstrap";

async function loginUser(credentials) {
    return fetch('http://localhost:3001/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(credentials)
    })
      .then(data => data.json())
}

const Login = ({setToken}) => {
    console.log("setToken: ", setToken);

    const [username, setUserName] = useState();
    const [password, setPassword] = useState();

    const handleSubmit = async e => {
        e.preventDefault();
        const token = await loginUser({
          username,
          password
        });
        setToken(token);
      }



  return (
    <>
      <Col lg="5" md="7">
        <Card className="bg-secondary shadow border-0">
        <CardBody className="px-lg-5 py-lg-5">
        <form onSubmit={handleSubmit}>
              <FormGroup className="mb-3">
                <InputGroup className="input-group-alternative">
                  <InputGroupAddon addonType="prepend">
                    <InputGroupText>
                      <i className="ni ni-email-83" />
                    </InputGroupText>
                  </InputGroupAddon>
                  <input placeholder="Email" type="text" onChange={e => setUserName(e.target.value)} />
                </InputGroup>
              </FormGroup>
              <FormGroup>
                <InputGroup className="input-group-alternative">
                  <InputGroupAddon addonType="prepend">
                    <InputGroupText>
                      <i className="ni ni-lock-circle-open" />
                    </InputGroupText>
                  </InputGroupAddon>
                  <input placeholder="Password" type="password" onChange={e => setPassword(e.target.value)} />
                </InputGroup>
              </FormGroup>
              
              <div className="text-center">
                <button className="my-4" color="primary" type="submit">Sign in</button>
              </div>
            </form>

          </CardBody>
        </Card>
        <Row className="mt-3">
          <Col xs="6">
            <a
              className="text-light"
              href="#pablo"
              onClick={(e) => e.preventDefault()}
            >
              <small>Forgot password?</small>
            </a>
          </Col>
          <Col className="text-right" xs="6">
            <a
              className="text-light"
              href="#pablo"
              onClick={(e) => e.preventDefault()}
            >
              <small>Create new account</small>
            </a>
          </Col>
        </Row>
      </Col>
    </>
  );
};

Login.propTypes = {
    setToken: PropTypes.func.isRequired
  };

export default Login;
