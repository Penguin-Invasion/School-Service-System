
import { Link } from "react-router-dom";
import {
    Button,
    Card,
    CardHeader,
    CardBody,
    FormGroup,
    Form,
    Input,
    Container,
    Row,
    Col,
  } from "reactstrap";
  // core components
  import UserHeader from "components/Headers/UserHeader.js";
  import { useState, useEffect } from "react";

  import useToken from '../../useToken'

  const Profile = () => {

    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [lastName, setLastName] = useState('');

    const { token } = useToken();

    function handleSubmit(event) {
      //event.preventDefault();
      console.log('name:', name);
      console.log('lastName:', lastName);
      console.log('email:', email);
      console.log('password:', password);

      // create a credentials object
    const credentials = {}
        // add the values to the credentials object
        // if values are not empty
        if (name) credentials.name = name;
        if (lastName) credentials.surName = lastName;
        if (email) credentials.email = email;
        if (password) credentials.password = password;

        console.log('credentials:', credentials);
      editProf(credentials);
    }

    async function editProf(credentials) {
        return fetch('https://schoolservicesystem.azurewebsites.net/api/Profile', {
          method: 'PATCH',
          headers: {
            // set token
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
            },
          body: JSON.stringify(credentials)
        })
          .then(data => data.json())
    }





    return (
      <>
        <UserHeader />
        {/* Page content */}
        <Container className="mt--7  " fluid>
          <Row>
            <Col className="order-xl-1" xl="8">
            <CardHeader className="bg-white border-0">
                  <Row className="align-items-center">
                    <Col xs="8">
                      <h3 className="mb-0">Profil Düzenle</h3>
                    </Col>
                    
                  </Row>
                </CardHeader>
              <Card className="bg-secondary shadow">
                <CardBody>
                <form onSubmit={handleSubmit}>
                <h6 className="heading-small text-muted mb-4">
                Bu ekrandan, bilgilerinizi güncelleyebilirsiniz. Sadece değişmesini istdiğiniz bilgileri gönderebilirsiniz.
                </h6>
                <div className="pl-lg-4">
                      <Row>
                        <Col lg="6">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-username"
                            >
                                Ad
                            </label>
                            <Input
                              className="form-control-alternative"
                              id="name"
                              placeholder="Ad"
                            type="text"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            />
                          </FormGroup>
                        </Col>
                        <Col lg="6">
                        <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-last-name"
                            >
                              Soyad
                            </label>
                            <Input
                              className="form-control-alternative"
                              
                              placeholder="Soyad"
                              id="lastName"
                            type="text"
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                            />
                          </FormGroup>
                        </Col>
                      </Row>
                      <Row>
                        <Col lg="6">
                        <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-email"
                            >
                              E Posta Adresi
                            </label>
                            <Input
                              className="form-control-alternative"
                              id="input-email"
                              placeholder="ornek@gmail.com"
                              type="text"
                              id="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                              
                            />
                          </FormGroup>
                        </Col>
                        <Col lg="6">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-last-name"
                            >
                              Yeni Şifre
                            </label>
                            <Input
                              className="form-control-alternative"
                              
                              placeholder="Şifre"
                              id="userName"
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                            />
                          </FormGroup>
                        </Col>
                      </Row>
                    </div>


                    <div>
                       
                    <Button type="submit" className="add-service">
                        Gönder
                    </Button>
                    </div>

                </form>
                </CardBody>
              </Card>
            </Col>
          </Row>
        </Container>
      </>
    );
  };

  export default Profile;
