import Header from "components/Headers/Header.js";

import { Card, CardBody, Button, Col, Container, CardHeader, Row, Table } from 'reactstrap';
// className="order-xl-1" xl="8"
const ServiceInfo = () => {
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
            <p>Servis Adı</p>
        </div>
        <div>
            <h3 className="mb-0 very-light-color">Servis Plakası:</h3>
            <p>Servis Adresi</p>
        </div>
        <div>
            <h3 className="mb-0 very-light-color">Sürücü İsmi:</h3>
            <p>Servis Adresi</p>
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
