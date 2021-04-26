from time import sleep

import setup_path 
import airsim

server = airsim.CarClient(port=41450)
status = server.ping()
print(status)
server.simPrintTest("This is the important test")
pose = airsim.Pose(airsim.Vector3r(0, 0, 0), airsim.to_quaternion(0, 0, 0))
server.simAddVehicle("Test", "PhysXCar", pose)
server.simAddPedestrian("TestPedestrian", pose)
sleep(3)
server.simRemoveVehicle("Test", "PhysXCar")
server.simRemovePedestrian("TestPedestrian")
# for i in range(5):
#     server.simAddVehicle("Test", "PhysXCar", pose)
#     sleep(2)
#     client = airsim.CarClient(port=41451)
#     client.confirmConnection()
#     client.enableApiControl(True, "Test")
#     car_controls = airsim.CarControls()
#     car_controls.throttle = 0.5
#     car_controls.steering = 0
#     client.setCarControls(car_controls, "PhysXCar", "Test")
#     client.setCarControls(car_controls, "PhysXCar", "Test")
#     sleep(5)
#     server.simRemoveVehicle("Test", "PhysXCar")