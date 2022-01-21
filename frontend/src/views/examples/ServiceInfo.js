import Header from "components/Headers/Header.js";

import { Card, CardBody, Button, Col, Container, CardHeader, Row, Table } from 'reactstrap';

import { useState, useEffect } from "react";

import useToken from '../../useToken'

const a = async (schoolId, serviceId, token)  => {
    const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + schoolId + '/Service/' + serviceId, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    })

    const body = await result.json()

    console.log("log?")

    if (body.success) {
        console.log("body??????", body);
        setServiceName(body.data.name);
        setServicePlaque(body.data.plaque);
        setDriverName(body.data.driver.name + ' ' + body.data.driver.surName);
        setStudents(body.data.students);
    }

}


const ServiceInfo = (props) => {
    const [ serviceName, setServiceName ] = useState('');
    const [ servicePlaque, setServicePlaque ] = useState('');
    const [ driverName, setDriverName] = useState('');
    const [ students, setStudents ] = useState([]);
    const { token } = useToken();

    const schoolId = props.match.params.schoolId;
    const serviceId = props.match.params.id;


    const getService = async ()  => {
        const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + schoolId + '/Service/' + serviceId, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }
        })
    
        const body = await result.json()
    
        if (body.success) {
            setServiceName(body.data.name);
            setServicePlaque(body.data.plaque);
            setDriverName(body.data.driver.name + ' ' + body.data.driver.surName);
            setStudents(body.data.students);
        }
    
    }

    useEffect(() => {
        getService();
    }, [])


    return (
        <>
        <Header />
      {/* Page content */}    
      <Container className="mt--7  " fluid>
      <Row>
        <Col className="order-xl-1" xl="8">
        <CardHeader className="table-head-color border-0">
        <Row className="align-items-center">
        <Col xs="8">
            <h3 className="mb-0 very-light-color">Servis Bilgileri</h3>
            <p>Bu ekrandan, servis bilgielrine bakabilir, servisi silebilir ve yeni öğrenciler ekleyebilirsiniz.</p>
        </Col>
        <Col className="text-right" xs="4">
            <Button color="warning" size="sm">Servisi Kaldır</Button>
        </Col>
        </Row>
        </CardHeader>
        <Card  className="bg-success shadow">
            
        <div className="table-service-info">
        <div>
            <h3 className="mb-0 very-light-color">Servis Adı:</h3>
            <p>{serviceName}</p>
        </div>
        <div>
            <h3 className="mb-0 very-light-color">Servis Plakası:</h3>
            <p> {servicePlaque} </p>
        </div>
        <div>
            <h3 className="mb-0 very-light-color">Sürücü İsmi:</h3>
            <p> {driverName} </p>
        </div>
        

        </div>
    <CardHeader className="table-head-color-student border-0">
        <Row className="align-items-center">
        <Col xs="8">
            <h3 className="mb-0 very-light-color">Öğrenci Bilgileri</h3>
            <p>Öğrenci bilgileri listelenmektedir. Yeni Öğrenci eklemek için butonu kullanabilirsiniz.</p>
        </Col>
        <Col className="text-right" xs="4">
            <Button color="info" size="sm">Yeni Öğrenci Ekle</Button>
        </Col>
        </Row>
    </CardHeader>
    <Table className="table-content-color-student" borderless>
        <thead>
            <tr>
            <th>Öğrenci İsmi</th>
            <th>Öğrenci Soyismi</th>
            <th>Giriş Yılı</th>
            <th>Öğrenciyi Sil</th>
            </tr>
        </thead>
        <tbody>
            <tr>
            <td>Mark</td>
            <td>Otto</td>
            <td>@mdo</td>
            <td><Button color="danger" size="sm">Sil</Button></td>
            </tr>
        </tbody>
    </Table>
    
        </Card>
        </Col>
        </Row>
    </Container>
        </>
    )
}

export default ServiceInfo
