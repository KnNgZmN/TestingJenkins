pipeline {
    agent any

    stages {

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --no-restore'
            }
        }

        stage('Test') {
            steps {
                bat '''
                    echo === Ejecutar pruebas y generar archivo TRX ===
                    dotnet test --no-build --logger "trx;LogFileName=test_results.trx" --results-directory TestResults

                    echo === Verificando archivos TRX generados ===
                    dir TestResults

                    echo === Creando manifiesto local de herramientas (si no existe) ===
                    if not exist .config (
                        mkdir .config
                    )
                    if not exist .config\\dotnet-tools.json (
                        dotnet new tool-manifest
                    )

                    echo === Instalando trx2junit localmente ===
                    dotnet tool install trx2junit --version 1.* || echo "trx2junit ya instalado"

                    echo === Ejecutando conversión TRX -> XML (JUnit) ===
                    dotnet tool run trx2junit TestResults\\*.trx

                    echo === Verificando archivos XML generados ===
                    dir TestResults
                '''
            }
            post {
                always {
                    junit 'TestResults/*.xml'
                }
            }
        }

        stage('Publish') {
            steps {
                bat '''
                    echo === Publicando artefactos ===
                    dotnet publish -c Release -o out
                '''
                archiveArtifacts artifacts: 'out/**', fingerprint: true
            }
        }
    }

    post {
        success {
            echo '✅ Compilación, pruebas y publicación completadas con éxito.'
        }
        failure {
            echo '❌ Algo falló durante la compilación o las pruebas.'
        }
    }
}
