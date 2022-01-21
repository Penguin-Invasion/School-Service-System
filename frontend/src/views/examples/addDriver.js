
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

  const Profile = () => {
    const [driverName, setDriverName] = useState('');
    const [driverLastName, setDriverLastName] = useState('');
    const [plaque, setPlaque] = useState('');


    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');

  function handleSubmit(event) {
    event.preventDefault();
    console.log('name:', name);
    console.log('message:', message);
    console.log('driverName:', driverName);

    createService({
      "id":Math.random(),
     "name":name,
     "message":message,
     "driverName":driverName
   });
  }

  async function createService(credentials) {
      return fetch('http://localhost:3001/drivers', {
        method: 'POST',
        headers: {
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
                      <h3 className="mb-0">Sürücü ve Servis Ekle</h3>
                    </Col>
                    
                  </Row>
                </CardHeader>
              <Card className="bg-secondary shadow">
                <CardBody>
                <form onSubmit={handleSubmit}>
                <h6 className="heading-small text-muted mb-4">
                Bu ekrandan, yeni sürücü ve plakasını ekleyebilirsiniz.
                </h6>
                <div className="pl-lg-4">
                      <Row>
                        <Col lg="6">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-username"
                            >
                                Sürücü Adı
                            </label>
                            <Input
                              className="form-control-alternative"
                              id="name"
                              placeholder="Ad"
                            type="text"
                            value={driverName}
                            onChange={(e) => setDriverName(e.target.value)}
                            />
                          </FormGroup>
                        </Col>
                        <Col lg="6">
                        <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-last-name"
                            >
                              Sürücü Soyadı
                            </label>
                            <Input
                              className="form-control-alternative"
                              
                              placeholder="Soyad"
                              id="lastName"
                            type="text"
                            value={driverLastName}
                            onChange={(e) => setDriverLastName(e.target.value)}
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
                              Plaka
                            </label>
                            <Input
                              className="form-control-alternative"
                              id="input-email"
                              placeholder="34ABC25"
                              type="text"
                              id="email"
                                value={plaque}
                                onChange={(e) => setPlaque(e.target.value)}
                              
                            />
                          </FormGroup>
                        </Col>
                        <Col lg="6">
                          
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
