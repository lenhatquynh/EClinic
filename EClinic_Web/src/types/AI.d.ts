export interface MachineLearning {
  MachineID: string
  MachineName: string
}
export interface DeepLearning {
  DeepID: string
  DeepName: string
}
export interface Model {
  ModelID: string
  ModelName: string
  Accuracy: number
  IsActive: boolean
  MachineLearning: MachineLearning
  DeepLearning: DeepLearning
}
export interface PredictHistory {
  PredictID: string
  Result: "Monkeypox" | "Chickenpox" | "Measles" | "Normal" | "Unknown"
  PredictTime: string
  ModelName: string
}
export interface PredictModel {
  file: any
  Note: string
  ModelID?: string
}
