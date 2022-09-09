import Head from 'next/head'
import styles from '../styles/Home.module.css'
import Swal from 'sweetalert2' // Alert mas chulos
import { useRef } from 'react'

export default function Home() {
  const url = useRef('');// Input url sin Salt
  const urlSalt = useRef(''); // Input url con Salt

  async function verificarUrl(e) {
    e.preventDefault();
    // Verificando que el campo no este vacio y tenga un largo de mas de 10
    if (url.current.value != "" && urlSalt.current.value.length > 10) {
      const response = await fetch(
        'https://localhost:7053/api/VerificarUrl',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            Url: urlSalt.current.value
          })
        }
      );

      //Control error
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const respuesta = await response.json();

      // Si el Url esta bien
      if (respuesta !== false) {
        Swal.fire(
          'Ok!',
          'Está todo correcto!',
          'success'
        );
      }
      else {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Link corrompido!'
        });
      }
    }
    else{
        Swal.fire(
          'Oops!',
          'Campo vacio o Link corrompido!.',
          'error'
        );
    }

  }

  // Funcion que envia url a la API para generar QR
  async function generarQR(e) {
    e.preventDefault();
    // Verificando que el campo no este vacio y tenga un largo de mas de 7
    if (url.current.value != "" && url.current.value.length > 7) {
      const response = await fetch(
        'https://localhost:7053/api/MandarUrl',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            Url: url.current.value
          })
        }
      );

      // Control error
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      if (response.ok) {
        Swal.fire(
          'Enviado!',
          'QR creado correctamente!',
          'success'
        );

        // Para refrescar la pagina y se vea el nuevo QR
        window.location.href = '/';
      }
    }
    else {
      Swal.fire(
        'Oops!',
        'Url no valida.',
        'error'
      )
    }
  }

  return (
    <div>
      <Head>
        <title>Salt_QR</title>
        <link rel="icon" href="/favicon.ico" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx" crossOrigin="anonymous" />
      </Head>

      <div className={styles.container}>
        <h1 className={styles.titulo}>Salt + QRCode</h1>
        <div className="row align-items-start">
          <div className="col">
            <div className="mb-3">
              <label className="form-label badge text-bg-danger">Url</label>
              <input ref={url} type="email" className="form-control" />
            </div>

            <button onClick={generarQR} className="btn btn-primary">Crear</button>
            <img src='MyQR.png' alt='QR' className={styles.qr} />
          </div>

          <div className="col">
            <div className="mb-3">
              <label className="form-label badge text-bg-danger">Url Con Todo Y Salt</label>
              <input ref={urlSalt} type="email" className="form-control" />
            </div>
            <button className="btn btn-success" onClick={verificarUrl} >Verificar</button>
          </div>

        </div>

      </div>
    </div>
  )
}
