import { useMutation, useQuery } from "@tanstack/react-query"
import { aiService } from "services/ai.service"
import { QUERY_KEYS, ROLE } from "shared/constant/constant"
import { DeepLearning, MachineLearning, Model, PredictModel } from "types/AI"

export type ModelAction = Omit<
  Model,
  "MachineLearning" | "DeepLearning" | "IsActive"
> & {
  MachineID: string
  DeepID: string
  file: any
}

export const useGetAllMachineLearningQuery = () => {
  const queryKey = [QUERY_KEYS.AI.MachineLearning]
  return useQuery({
    queryKey,
    queryFn: () => aiService.getAllMachineLearning()
  })
}

export const useUpdateMachineLearningMutation = () =>
  useMutation({
    mutationFn: (data: MachineLearning) => aiService.updateMachineLearning(data)
  })
export const useCreateMachineLearningMutation = () =>
  useMutation({
    mutationFn: (data: string) => aiService.createMachineLearning(data)
  })
export const useGetAllDeepLearningQuery = () => {
  const queryKey = [QUERY_KEYS.AI.DeepLearning]
  return useQuery({
    queryKey,
    queryFn: () => aiService.getAllDeepLearning()
  })
}
export const useUpdateDeepLearningMutation = () =>
  useMutation({
    mutationFn: (data: DeepLearning) => aiService.updateDeepLearning(data)
  })
export const useCreateDeepLearningMutation = () =>
  useMutation({
    mutationFn: (data: string) => aiService.createDeepLearning(data)
  })
//Model
export const useGetAllModelQuery = () => {
  const queryKey = [QUERY_KEYS.AI.Model]
  return useQuery({
    queryKey,
    queryFn: () => aiService.getAllModel()
  })
}
export const useCreateModelMutation = () =>
  useMutation({
    mutationFn: (data: ModelAction) => aiService.createModel(data)
  })
export const useUpdateModelMutation = () =>
  useMutation({
    mutationFn: (data: ModelAction) => aiService.updateModel(data)
  })
export const useActiveModelMutation = () =>
  useMutation({
    mutationFn: (modelId: string) => aiService.activeModel(modelId)
  })
//History
export const useGetAllHistory = (pageNumber: number, pageSize: number) => {
  const queryKey = [QUERY_KEYS.AI.History, pageNumber, pageSize]
  return useQuery({
    queryKey,
    queryFn: () => aiService.getAllHistory(pageNumber, pageSize)
  })
}
//Predict
export const usePredictMutation = (role: string) =>
  useMutation({
    mutationFn: (data: PredictModel) =>
      role === ROLE.EXPERT
        ? aiService.expertPredict(data)
        : aiService.doctorPredict(data)
  })
