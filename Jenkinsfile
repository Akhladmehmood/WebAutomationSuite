pipeline {
    agent any

    options {
        timestamps()
        disableConcurrentBuilds()
        buildDiscarder(logRotator(numToKeepStr: '10'))
    }

    parameters {
        choice(
            name: 'ENV',
            choices: ['dev', 'qa', 'staging'],
            description: 'Select environment'
        )

        choice(
            name: 'TEST_TYPE',
            choices: ['Smoke', 'Regression'],
            description: 'Select test suite'
        )
    }

    environment {
        GIT_REPO_URL = 'https://github.com/Akhladmehmood/WebAutomationSuite.git'
        GIT_BRANCH   = 'AddjenkinsFile'
        REPORT_DIR   = 'TestResults/ExtentReports'
    }

    stages {

        stage('Checkout Code') {
            steps {
                cleanWs()
                git branch: "${GIT_BRANCH}",
                    url: "${GIT_REPO_URL}"
            }
        }

        stage('Restore Dependencies') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build Solution') {
            steps {
                bat 'dotnet build --no-restore'
            }
        }

        stage('Execute Tests') {
            steps {
                bat """
                dotnet test ^
                --no-build ^
                --filter TestCategory=${params.TEST_TYPE} ^
                --logger trx
                """
            }
        }
    }

    post {

        always {
            echo 'Publishing Extent Report'

            publishHTML([
                allowMissing: true,
                alwaysLinkToLastBuild: true,
                keepAll: true,
                reportDir: "${REPORT_DIR}",
                reportFiles: 'index.html',
                reportName: "Automation Report - ${params.ENV} - ${params.TEST_TYPE}"
            ])
        }

        success {
            echo 'BUILD SUCCESSFUL'
        }

        failure {
            echo 'BUILD FAILED - CHECK REPORTS'
        }
    }
}
