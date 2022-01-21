
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

const getSchool = async (credentials, token) => {
    const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    })

    const body = await result.json()
    //console.log("body? ", body)
    if (body.success) {
        createDriver(credentials, token, body.data[0].id);
    }

}


async function createDriver(credentials, token, schoolID) {
    // create driver object
    const driver = {}
    driver.name = credentials.name;
    driver.surName = credentials.surName; 
    driver.email = "email@gmail.com";
    driver.password = "password";
    const service = {}
    service.name = credentials.serviceName;
    service.plaque = credentials.plaque;


    const response = await fetch('https://schoolservicesystem.azurewebsites.net/api/Manager/CreateDriver', {
        method: 'POST',
        headers: {
            // set token
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS'
            },
        body: JSON.stringify(driver)
    })
    const body  = await response.json();


    // if body.status success, call createService func
    if (body.success) {
        service.schoolID = schoolID;
        service.driverID = body.data.id;
        createService(service, token);
    }
}

async function createService(service, token) {

    const response = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + service.schoolID + '/Service', {
        method: 'POST',
        headers: {
            // set token
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
            },
        body: JSON.stringify(service)
    })
    const body  = await response.json();

    return body;
    //console.log("add service response:", body);
    
}

const Profile = () => {
    
    const { token } = useToken();
    
    const [driverName, setDriverName] = useState('');
    const [driverLastName, setDriverLastName] = useState('');
    const [plaque, setPlaque] = useState('');
    const [serviceName, setServiceName] = useState('');
    const [ added, setAdded ] = useState(0);


    function handleSubmit(event) {
        event.preventDefault();
        // console.log('driverName:', driverName);
        // console.log('driverLastName:', driverLastName);
        // console.log('serviceName:', serviceName);
        // console.log('plaque:', plaque);

        // create a credentials object
        const credentials = {}
        // add the values to the credentials object
        // if values are not empty
        if (driverName) credentials.name = driverName;
        if (driverLastName) credentials.surName = driverLastName;
        if (plaque) credentials.plaque = plaque;
        if (serviceName) credentials.serviceName = serviceName;

        // clear the form
        setDriverName('');
        setDriverLastName('');
        setPlaque('');
        setServiceName('');

        // if credentials are not empty
        if (credentials.name && credentials.surName && credentials.plaque && credentials.serviceName) {
            getSchool(credentials, token);
            setAdded(1);
        }
        else {
            setAdded(-1);
        }
        

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
                                Servis Adı
                            </label>
                            <Input
                                className="form-control-alternative"
                                id="input-email"
                                placeholder="Şahin"
                                type="text"
                                id="email"
                                value={serviceName}
                                onChange={(e) => setServiceName(e.target.value)}
                                
                            />
                            </FormGroup>
                        </Col>
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
                        </Row>
                    </div>


                    <div>
                        
                    <Row>
                    <Button type="submit" className="add-service">
                        Ekle
                    </Button>
                    </Row>
                    {added === 1 && <p>Sürücü ve Servis eklendi</p>}
                    {added === -1 && <p>Sürücü ve Servis eklenemedi</p>}
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
