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
import { data } from "jquery";


// 'content-encoding': 'gzip', 
// 'content-type': 'application/json; charset=utf-8', 
// 'date': 'Mon,17 Jan 2022 09:01:22 GMT',
// 'server': 'Microsoft-IIS/10.0' ,
// 'transfer-encoding': 'chunked', 
// 'vary': 'Accept-Encoding' ,
// 'x-powered-by': 'ASP.NET'

async function loginUser(credentials) {
    const response = await fetch('https://schoolservicesystem.azurewebsites.net/api/Auth', {
      method: 'POST',
      headers: {
          // Access-Control-Allow-Origin
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(credentials)
    })
    
    const body  = await response.json();
    if (body.success) {
        return body.data.token;
    }

    return null;

}

// loginUser func
async function c(credentials) {
    const response = await fetch('http://localhost:3001/login');
    const body = await response.json();

    // iterate through body.data and check if credentials match
    for (let i = 0; i < body.length; i++) {
        if (body[i].email === credentials.email && body[i].password === credentials.password) {
            return body[i].type;
        }
    }

    return null;
        
} 

const Login = ({setToken}) => {





    const [username, setUserName] = useState();
    const [password, setPassword] = useState();
    const [loginAttempt, setLoginAttempt] = useState(false);

    const handleSubmit = async e => {
        e.preventDefault();
        const email = username;
        const res = await loginUser({
            email,
            password
        });
        console.log(res);
        setToken(res);
        
        // sleep for 2 seconds
        await new Promise(resolve => setTimeout(resolve, 1100));
        setLoginAttempt(true);
      }



  return (
    <>
      <Col lg="5" md="7">
        <Card className="bg-secondary shadow border-0">
        <CardBody className="px-lg-5 py-lg-5">
        <form onSubmit={handleSubmit}>
              <FormGroup className="mb-3">
              {loginAttempt  && <h2>Geçersiz Kullanıcı Adı - Şifre</h2>}
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
