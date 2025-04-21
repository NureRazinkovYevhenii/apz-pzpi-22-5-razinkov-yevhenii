# Обробка подій на Python. Приклад коду патерну "Обробка подій".
import pika
import json


def process_new_photo(ch, method, properties, body):
    event = json.loads(body)
    photo_id = event['photo_id']
    # Тут може бути логіка обробки зображення
    print(f"Обробляємо нове фото з ID: {photo_id}")

connection = pika.BlockingConnection(pika.ConnectionParameters('rabbitmq'))
channel = connection.channel()
channel.queue_declare(queue='new_photos')

channel.basic_consume(queue='new_photos',
                      on_message_callback=process_new_photo,
                      auto_ack=True)

print('Очікуємо нові фото...')
channel.start_consuming()


# Кешування даних у Redis. Приклад коду "Кеш збоку"
import redis

cache = redis.Redis(host='localhost', port=6379, db=0)

def get_user_profile(user_id):
    profile = cache.get(f"user:{user_id}")
    if profile:
        return profile  # Повертаємо з кешу
    # Якщо в кеші немає — отримуємо з БД (імітація)
    profile = db_get_user_profile(user_id)
    cache.set(f"user:{user_id}", profile, ex=3600)  # Кладемо в кеш на годину
    return profile
