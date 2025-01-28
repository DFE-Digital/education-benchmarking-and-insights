import { useRef, useState } from "react";
import { ModalSpikeProps } from ".";
import { Modal } from "../modal";

export function ModalSpike({ ...props }: ModalSpikeProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [count, setCount] = useState<number>();
  const button = useRef<HTMLButtonElement>(null);

  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    window.setTimeout(function () {
      button.current?.focus();
    }, 0);
  };

  return (
    <div>
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        onClick={handleOpenModal}
        ref={button}
      >
        Open modal {count && count}
      </button>
      {isModalOpen && (
        <Modal
          cancel
          ok
          onClose={handleCloseModal}
          onOK={() => {
            setCount((count || 0) + 1);
            handleCloseModal();
          }}
          title="This is the modal title"
          {...props}
        >
          This is the modal body
        </Modal>
      )}
    </div>
  );
}
