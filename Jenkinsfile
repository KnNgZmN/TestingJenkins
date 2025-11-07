pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                bat '''
                    echo === Compilando solución ===
                    dotnet build PruebaTunit.sln --configuration Debug
                '''
            }
        }

        stage('Test') {
            steps {
                bat '''
                    echo === Ejecutar pruebas y generar archivo TRX ===
                    dotnet test testTunit/testTunit.csproj --no-build --logger "trx;LogFileName=test_results.trx" --results-directory testTunit/TestResults

                    echo === Verificando archivos TRX generados ===
                    dir testTunit\\TestResults

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
                    dotnet tool run trx2junit testTunit\\TestResults\\*.trx

                    echo === Verificando archivos XML generados ===
                    dir testTunit\\TestResults
                '''
            }
            post {
                always {
                    echo '=== Publicando resultados de pruebas ==='
                    junit allowEmptyResults: true, testResults: 'testTunit/TestResults/*.xml'
                }
            }
        }
    }

    post {
        always {
            echo '=== Pipeline finalizado ==='
        }
    }
}
