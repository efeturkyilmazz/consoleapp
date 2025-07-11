pipeline {

    agent any
 
    environment {

        GIT_CREDENTIALS = credentials('Sonarqube login token')  // name of the Jenkins credential you created

        SONARQUBE_SCANNER = 'SonarScanner' // Jenkins > Global Tool Configuration ismi

        SONARQUBE_SERVER = 'SonarQubeServer' // Jenkins > Manage Jenkins > SonarQube ayarı
        SONARQUBE_TOKEN='sqa_143da33efb86e9882fcc26e54f652c1b737aa859'
        PROJECT_KEY = 'ConsoleApp'

        PROJECT_NAME = 'Console App'

    }   

    stages {
        stage('BOM Check & Lint') {
                steps {
                    echo '🔍 Running BOM and Lint checks...'
                    sh 'chmod +x ./ci-check.sh'
                    sh './ci-check.sh'
                }
            }

        stage('Checkout') {

            steps {

                script {

                 checkout scmGit(

                        branches: [[name: 'main']],

                        userRemoteConfigs: [[url: 'https://github.com/efeturkyilmazz/consoleapp.git']])

                }

            }

		}
 
		stage('Restore') {

			steps {

				sh 'dotnet restore'

			}

		}

 
        stage('SonarQube Analysis Begin') {

            steps {

                

                    withSonarQubeEnv("${SONARQUBE_SERVER}") {

                        sh """
                        echo "🔧 Installing dotnet-sonarscanner (if not exists)..."
                            dotnet tool install --global dotnet-sonarscanner || true
                            echo "step 2"
                            export PATH="\$PATH:/root/.dotnet/tools"
                            
                    echo "🚀 Starting SonarQube analysis..."
                    dotnet-sonarscanner begin \
                        /k:"'"${PROJECT_KEY}"'" \
                        /n:"'"${PROJECT_NAME}"'" \
                        /d:sonar.login=${SONARQUBE_TOKEN}

                        """

                    }

                

            }

        }
 
        stage('Build') {

            steps {

                sh 'dotnet build --configuration Release'

            }

        }
 
        stage('Test') {

            steps {

                sh 'dotnet test --no-build'

            }

        }
 stage('SonarQube Analysis End') {
    steps {
        
            withSonarQubeEnv("${SONARQUBE_SERVER}") {
                sh '''
                    echo "📦 Finalizing SonarQube analysis..."
                    export PATH="$PATH:/root/.dotnet/tools"
                    dotnet-sonarscanner end /d:sonar.login=${SONARQUBE_TOKEN}
                '''
            }
        
    }
}

 
        stage('Wait for Quality Gate') {

            steps {

                timeout(time: 2, unit: 'MINUTES') {

                    waitForQualityGate abortPipeline: true

                }

            }

        }

    }
 


}

 